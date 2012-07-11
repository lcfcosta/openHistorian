﻿//******************************************************************************************************
//  BasicTreeContainer.cs - Gbtc
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
//  5/22/2012 - Steven E. Chisholm
//       Generated original version of source code. 
//
//******************************************************************************************************

using System;
using openHistorian.V2.Collections.KeyValue;
using openHistorian.V2.FileSystem;
using openHistorian.V2.IO.Unmanaged;

namespace openHistorian.V2.Server.Database.Partitions
{
    /// <summary>
    /// Encapsolates the ArchiveFileStream, BinaryStream, and BasicTree for a certain tree.
    /// </summary>
    internal class BasicTreeContainer : IDisposable
    {
        #region [ Members ]

        ArchiveFileStream m_archiveStream;
        BinaryStream m_binaryStream;
        BasicTree m_tree;
        bool m_disposed;

        #endregion

        #region [ Constructors ]

        public BasicTreeContainer(TransactionalRead currentTransaction, Guid fileNumber, int flags)
        {
            m_archiveStream = currentTransaction.OpenFile(fileNumber, flags);
            m_binaryStream = new BinaryStream(m_archiveStream);
            m_tree = new BasicTree(m_binaryStream);
        }

        #endregion

        #region [ Properties ]

        public bool IsDisposed
        {
            get
            {
                return m_disposed;
            }
        }
        
        public ulong FirstKey
        {
            get
            {
                return m_tree.FirstKey;
            }
        }

        public ulong LastKey
        {
            get
            {
                return m_tree.LastKey;
            }
        }

        #endregion

        #region [ Methods ]

        public IDataScanner GetDataRange()
        {
            return m_tree.GetDataRange();
        }

        public void Dispose()
        {
            if (!m_disposed)
            {
                try
                {
                    if (m_binaryStream != null)
                    {
                        m_binaryStream.Dispose();
                    }
                    if (m_archiveStream != null)
                    {
                        m_archiveStream.Dispose();
                    }
                }
                finally
                {
                    m_archiveStream = null;
                    m_binaryStream = null;
                    m_tree = null;
                    m_disposed = true;
                }
            }
        }

        #endregion

    }
}
