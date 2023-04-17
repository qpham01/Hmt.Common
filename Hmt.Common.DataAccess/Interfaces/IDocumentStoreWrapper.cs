using Hmt.Common.DataAccess.Database;
using Marten;

namespace Hmt.Common.DataAccess.Interfaces;

public interface IDocumentStoreWrapper
{
    ISessionWrapper OpenSession();
}
