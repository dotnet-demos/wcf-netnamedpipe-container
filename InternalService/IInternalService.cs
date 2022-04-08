﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace InternalService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IInternalService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        double GetValueOfPi(int timeoutInSeconds);

        // TODO: Add your service operations here
    }
}
