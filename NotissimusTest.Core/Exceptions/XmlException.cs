namespace NotissimusTest.Core.Exceptions;

public class XmlException : Exception
{
    public XmlException()
    {
    }

    public XmlException(string message)
        : base(message)
    {
    }
}