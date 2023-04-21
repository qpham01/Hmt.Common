namespace Hmt.Common.DataAccess.Interfaces;

public interface IDocumentStoreWrapper
{
    ISessionWrapper OpenSession();
}
