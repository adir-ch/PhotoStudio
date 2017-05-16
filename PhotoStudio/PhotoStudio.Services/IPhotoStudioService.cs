using PhotoStudio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Services
{
    [ServiceContract]
    public interface IPhotoStudioService
    {
        [OperationContract]
        List<Photo> GetPhotos();

        [OperationContract]
        void SubmitPrintOrder(PrintOrder order); 
    }
}
