﻿using HWS.Dal.Entities;
using HWS.Dal.Sql.MongoUsers.JoinTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.DbEntities
{
    public class HomeworkGrader : MongoUser
    {
        public ICollection<HomeworkGranderJoin> Homeworks { get; set; }
    }
}
