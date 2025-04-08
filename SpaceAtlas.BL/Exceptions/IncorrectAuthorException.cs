namespace SpaceAtlas.BL.Exceptions;
[Serializable]
public class IncorrectAuthorException : Exception
{
    public IncorrectAuthorException() : base() {}
    public IncorrectAuthorException(string message) : base(message) {}
    public IncorrectAuthorException(string message, Exception inner) : base(message, inner) {}
}