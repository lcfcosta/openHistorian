﻿//******************************************************************************************************
//  ProcessGenerationRolloverSettings.cs - Gbtc
//
//  Copyright © 2012, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  7/4/2012 - Steven E. Chisholm
//       Generated original version of source code. 
//       
//
//******************************************************************************************************

using System;
using System.Data;
using System.IO;

namespace openHistorian.V2.Server.Database
{
    class ProcessGenerationRolloverSettings
    {
        public int AutoRolloverCount;
        public TimeSpan AutoRolloverInterval;
        public long AutoRolloverSize;

        public ProcessGenerationRolloverSettings()
        {
        }

        public ProcessGenerationRolloverSettings(BinaryReader reader)
        {
            Load(reader);
        }

        public void Load(BinaryReader reader)
        {
            switch (reader.ReadByte())
            {
                case 0:
                    AutoRolloverCount = reader.ReadInt32();
                    AutoRolloverInterval = new TimeSpan(reader.ReadInt64());
                    AutoRolloverSize = reader.ReadInt64();
                    break;
                default:
                    throw new VersionNotFoundException();
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write((byte)0);
            writer.Write(AutoRolloverCount);
            writer.Write(AutoRolloverInterval.Ticks);
            writer.Write(AutoRolloverSize);
        }
    }
}
