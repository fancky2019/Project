using System;
using System.Collections.Generic;
using System.Text;

namespace ZDFixService.Utility
{
    class ClOrderIDGenerator
    {
        long _beginOrderId = 0;
        long _endOrderId = 0;
        long _lastClientOrderID;
        SnowFlake _snowFlake = null;

        internal ClOrderIDGenerator(long lastClientOrderID)
        {
            this._lastClientOrderID = lastClientOrderID;

            var cliOrderIDScope = Configurations.Configuration["ZDFixService:CliOrderIDScope"].ToString();
            if (string.IsNullOrEmpty(cliOrderIDScope))
            {
                throw new Exception("Order_ID_Scope is null!");
            }
            _beginOrderId = long.Parse(cliOrderIDScope.Split(',')[0]);
            _endOrderId = long.Parse(cliOrderIDScope.Split(',')[1]);

            var useSnowFlakeConfig = Configurations.Configuration["ZDFixService:SnowFlake:UseSnowFlake"];
            if (!string.IsNullOrEmpty(useSnowFlakeConfig))
            {
                if (bool.TryParse(useSnowFlakeConfig, out bool useSnowFlake))
                {
                    if (useSnowFlake)
                    {
                        var workerID = int.Parse(Configurations.Configuration["ZDFixService:SnowFlake:WorkerID"]);
                        var startDate = DateTime.Parse(Configurations.Configuration["ZDFixService:SnowFlake:StartDate"]);
                        var sequenceBits = int.Parse(Configurations.Configuration["ZDFixService:SnowFlake:SequenceBits"]);
                        _snowFlake = new SnowFlake(workerID, startDate, sequenceBits);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal long GetNextClOrderID()
        {

            if (_snowFlake == null)
            {
                _lastClientOrderID++;
                _lastClientOrderID = _lastClientOrderID <= _beginOrderId ? _beginOrderId + 1 : _lastClientOrderID;
                _lastClientOrderID = _lastClientOrderID >= _endOrderId ? _beginOrderId + 1 : _lastClientOrderID;
            }
            else
            {
                _lastClientOrderID = _snowFlake.NextId();
            }
            return _lastClientOrderID;
        }
    }
}
