﻿/* ====================================================================
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
==================================================================== */

/* ================================================================
 * About NPOI
 * Author: Tony Qu
 * Author's email: tonyqus (at) gmail.com
 * Author's Blog: tonyqus.wordpress.com.cn (wp.tonyqus.cn)
 * HomePage: http://www.codeplex.com/npoi
 * Contributors:
 *
 * ==============================================================*/

using System;
using System.Globalization;
using System.IO;

namespace Npoi.Core.Util
{
    /// <summary>
    /// representation of a byte (8-bit) field at a fixed location within a
    /// byte array
    /// @author Marc Johnson (mjohnson at apache dot org
    /// </summary>
    public class ByteField : FixedField
    {
        private const byte _default_value = 0;
        private int _offset;
        private byte _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteField"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public ByteField(int offset)
            : this(offset, (byte)0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteField"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="value">The value.</param>
        public ByteField(int offset, byte value)
        {
            if (offset < 0)
            {
                throw new IndexOutOfRangeException("offset cannot be negative");
            }
            _offset = offset;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteField"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="data">The data.</param>
        public ByteField(int offset, byte[] data)
            : this(offset)
        {
            ReadFromBytes(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteField"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="_value">The _value.</param>
        /// <param name="data">The data.</param>
        public ByteField(int offset, byte _value, byte[] data)
            : this(offset, _value)
        {
            WriteToBytes(data);
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual byte Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// set the value from its offset into an array of bytes
        /// </summary>
        /// <param name="data">the byte array from which the value is to be read</param>
        public virtual void ReadFromBytes(byte[] data)
        {
            _value = data[_offset];
        }

        /// <summary>
        /// set the value from an Stream
        /// </summary>
        /// <param name="stream">the Stream from which the value is to be read</param>
        public virtual void ReadFromStream(Stream stream)
        {
            //this._value = LittleEndian.ReadFromStream(stream, LittleEndianConsts.BYTE_SIZE)[0];
            int ib = stream.ReadByte();
            if (ib < 0)
            {
                throw new BufferUnderflowException();
            }
            _value = (byte)ib;
        }

        /// <summary>
        /// set the ByteField's current value and write it to a byte array
        /// </summary>
        /// <param name="value">value to be set</param>
        /// <param name="data">the byte array to write the value to</param>
        public virtual void Set(byte value, byte[] data)
        {
            Value = value;
            WriteToBytes(data);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.object"/>.
        /// </returns>
        public override string ToString()
        {
            return Convert.ToString(_value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// write the value out to an array of bytes at the appropriate offset
        /// </summary>
        /// <param name="data">the array of bytes to which the value is to be written</param>
        public virtual void WriteToBytes(byte[] data)
        {
            data[_offset] = _value;
        }
    }
}