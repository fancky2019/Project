using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entity
{
    public class Person
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? JobID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
    }
}