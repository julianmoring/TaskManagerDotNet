namespace TaskManager.Application.Commands;

public sealed record CreateLabelCommand(long BoardId, string Name, string Color);

public sealed record RenameLabelCommand(long LabelId, string NewName);

public sealed record RecolorLabelCommand(long LabelId, string NewColor);

public sealed record AttachLabelCommand(long CardId, long LabelId);

public sealed record DetachLabelCommand(long CardId, long LabelId);
