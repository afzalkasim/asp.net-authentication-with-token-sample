using System.Linq;
using System.Threading.Tasks;
using Glamz.Business.Entity;

namespace Glamz.Business.Repository
{
    public interface IGlamzDBContext
    {
        IQueryable<T> Table<T>(string collectionName);
        Task<byte[]> GridFSBucketDownload(string id);
        Task<string> GridFSBucketUploadFromBytesAsync(string filename, byte[] source);
        Task<bool> DatabaseExist(string connectionString);
        Task CreateTable(string name, string collation);
        Task CreateIndex<T>(IGlamzRepository<T> repository, OrderBuilder<T> orderBuilder, string indexName, bool unique = false) where T : BaseEntity;
    }
}
