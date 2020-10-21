using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestDal
{
    public class TClientBaseInfoDal
    {
        public List<TClientBaseInfo> GetTClientBaseInfos()
        {
            List<TClientBaseInfo> list = new List<TClientBaseInfo>();
            
            using (var db = new PressTestDbContext())
            {
                try
                {
                    list = db.TClientBaseInfo.ToList();
                    list.ForEach(p =>
                    {
                        if(p.FPassword== "ofxPYFAYi1k=")
                        {
                            p.FPassword = "888888";
                        }
                    });
                }
                catch(Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            return list;
        }

    }
}
