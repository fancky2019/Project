using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using TestDal;

namespace TestService.Services
{
    public class TClientBaseInfoService
    {
        TClientBaseInfoDal _clientBaseInfoDal = new TClientBaseInfoDal();
        public List<TClientBaseInfo> GetTClientBaseInfos()
        {
            List<TClientBaseInfo> list = new List<TClientBaseInfo>();
            list = _clientBaseInfoDal.GetTClientBaseInfos();
            return list;
        }
    }
}
