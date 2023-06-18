namespace DevArt.Core.Domain;

public interface IApplyEvent<TEvent, TAggregateState>
{
    TAggregateState Apply(TEvent @event);
}