using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;

namespace PlayGroundRT.Services
{
    public interface IUploaderService
    {
        DataSet UploadFiles(UploadedFile[] uploadedFiles);
    }

    [Service(Name = "UploaderService")]
    public class UploaderService : IUploaderService //, IInitializable, ISingleton
    {
        DataSet IUploaderService.UploadFiles(UploadedFile[] uploadedFiles)
        {
            IEntityManager em = EntityManager.FromDomain<Upload.DomainProvider>();

            foreach (UploadedFile uploadedFile in uploadedFiles)
            {
                var file = em.CreateInstance<Upload.FileUploaded>();

                file.Name = uploadedFile.Name;
                file.ContentType = uploadedFile.ContentType;
                file.Size = uploadedFile.ContentLength;
            }

            em.Data.AcceptChanges();

            return em.Data;
        }

    }

}
