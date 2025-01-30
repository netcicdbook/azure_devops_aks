namespace Components.Communication.Events
{
    public record BaseEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime CreationDate => DateTime.Now;
        public string EventType => GetType().FullName ?? "UnknownType";
    }
}
