namespace DevArt.Core.Domain;

public record EventMessage(string PersistenceId, object Event, long Offset, long SequenceNr, long Timestamp);