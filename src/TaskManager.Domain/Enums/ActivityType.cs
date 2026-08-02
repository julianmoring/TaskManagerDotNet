namespace TaskManager.Domain.Enums;

public enum ActivityType
{
    BoardCreated,
    BoardRenamed,
    BoardDeleted,
    ColumnAdded,
    ColumnRenamed,
    ColumnRemoved,
    CardCreated,
    CardMoved,
    CardUpdated,
    CardDeleted,
    LabelCreated,
    LabelAttached,
    LabelDetached,
    CommentAdded,
    SpecVersionCreated,
    SessionStarted,
    SessionStopped,
    SessionEnded,
    ChecklistCreated,
    ChecklistItemToggled
}
