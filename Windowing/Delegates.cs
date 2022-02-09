namespace Zene.Windowing
{
    public delegate void FileDropEventHandler(object sender, FileDropEventArgs e);

    public delegate void TextInputEventHandler(object sender, TextInputEventArgs e);

    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    public delegate void ScrolEventHandler(object sender, ScrollEventArgs e);

    public delegate void MouseEventHandler(object sender, MouseEventArgs e);

    public delegate void SizeChangeEventHandler(object sender, SizeChangeEventArgs e);

    public delegate void FocusedEventHandler(object sender, FocusedEventArgs e);

    public delegate void PositionEventHandler(object sender, PositionEventArgs e);
}
