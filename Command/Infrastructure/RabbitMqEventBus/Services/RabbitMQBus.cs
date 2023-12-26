using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using Command.Core.Domain.Core;
using Command.Core.Application.Repositories;
using Command.Core.Common;
namespace Command.Infrastructure.RabbitMqBus.Services;
public sealed class RabbitMQBus : IEventBus
{

    private readonly Dictionary<string, List<Type>> _handlers;

    private readonly List<Type> _eventTypes;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly RabbitMqConfig _rabbitMqConfig;

    public RabbitMQBus(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMqConfig> rabbitMqConig)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _rabbitMqConfig = rabbitMqConig.Value;
    }


    public void Publish<T>(T @event) where T : DomainEvent
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();
        connectionFactory.HostName = _rabbitMqConfig.HostName;
        connectionFactory.UserName = _rabbitMqConfig.UserName;
        connectionFactory.Password = _rabbitMqConfig.Password;

        using IConnection connection = connectionFactory.CreateConnection();
        IModel model = connection.CreateModel();
        IBasicProperties basicProperties = model.CreateBasicProperties();
        basicProperties.Persistent = true;
        using IModel model2 = connection.CreateModel();
        string name = @event.GetType().Name;
        model2.QueueDeclare(name, durable: true, exclusive: false, autoDelete: false, null);
        string s = JsonConvert.SerializeObject(@event, Formatting.Indented);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        model2.BasicPublish("", name, mandatory: true, basicProperties, bytes);
    }

    public void Subscribe<T, TH>() where T : DomainEvent where TH : IEventHandler<T>
    {
        string name = typeof(T).Name;
        Type handlerType = typeof(TH);
        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(name))
        {
            _handlers.Add(name, new List<Type>());
        }

        if (_handlers[name].Any((Type s) => s.GetType() == handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{name}'", "handlerType");
        }

        _handlers[name].Add(handlerType);
        StartBasicConsume<T>();
    }

    private void StartBasicConsume<T>() where T : DomainEvent
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();
        connectionFactory.HostName = _rabbitMqConfig.HostName;
        connectionFactory.UserName = _rabbitMqConfig.UserName;
        connectionFactory.Password = _rabbitMqConfig.Password;

        IConnection connection = connectionFactory.CreateConnection();
        IModel model = connection.CreateModel();
        string name = typeof(T).Name;
        model.QueueDeclare(name, durable: true, exclusive: false, autoDelete: false, null);
        var asyncEventingBasicConsumer = new AsyncEventingBasicConsumer(model);
        asyncEventingBasicConsumer.Received += Consumer_Received;
        model.BasicConsume(name, autoAck: true, asyncEventingBasicConsumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        string eventName = e.RoutingKey;
        string message = Encoding.UTF8.GetString(e.Body.ToArray());
        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(continueOnCapturedContext: false);
        }
        catch (Exception)
        {
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        string eventName2 = eventName;
        if (!_handlers.ContainsKey(eventName2))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        List<Type> subscriptions = _handlers[eventName2];
        foreach (Type subscription in subscriptions)
        {
            object handler = scope.ServiceProvider.GetService(subscription);
            if (handler != null)
            {
                Type eventType = _eventTypes.SingleOrDefault((Type t) => t.Name == eventName2);
                object @event = JsonConvert.DeserializeObject(message, eventType);
                Type conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[1] { @event });
            }
        }
    }
}