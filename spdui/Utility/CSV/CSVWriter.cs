/*
 * CSV I/O Library.NET
 * Copyright (C) 2005, uguu All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 
 *  - Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 
 *  - Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
 * ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Data;
using System.IO;

namespace Dndp.Utility.CSV
{
    /// <summary>
    ///     <see cref="TextWriter"/> ���󥹥��󥹤� CSV ��ʽ�Υǩ`����������ޤ���
    /// </summary>
    /// <remarks>
    ///     ���󥹥��󥹤����ɕr��ָ�������������֡����Ф����֤�ʹ�ä��� CSV �ǩ`����������ޤ���
    ///     �������낎�˸������֡����Ф����֡����֥륯���`�Ʃ`����󤬺��ޤ����ϡ��������`�פ��Ƴ������ޤ���
    ///     null ���դ� <see cref="DBNull.Value"/> ���Ͽ������ФȤ��Ƴ������ޤ���
    /// </remarks>
    public sealed class CSVWriter : IDisposable
    {
        /// <summary>
        ///     �����ȥ饤���`��
        /// </summary>
        private TextWriter writer;
        /// <summary>
        ///     �������֡�
        /// </summary>
        private string newLine;
        /// <summary>
        ///     ���Ф����֡�
        /// </summary>
        private string delimiter;
        /// <summary>
        ///     ������λ�ä������ǥå������š�
        /// </summary>
        private int columnIndex = 0;
        /// <summary>
        ///     ������λ�ä������ǥå������š�
        /// </summary>
        private int rowIndex = 0;
        /// <summary>
        ///     ָ������ <see cref="TextWriter"/> ���󥹥��󥹤� CSV ��ʽ�Υǩ`����������롢
        ///     <see cref="CSVWriter"/> ���饹���¤������󥹥��󥹤���ڻ����ޤ���
        ///     �������֤� <see cref="Environment.NewLine"/> �����Ф����֤� "," �ˤʤ�ޤ���
        /// </summary>
        /// <param name="writer">�����ȥ饤���`��</param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> ������ null ���դΈ��ϡ�</exception>
        public CSVWriter(TextWriter writer)
            : this(writer, Environment.NewLine, ",")
        {
        }
        /// <summary>
        ///     ָ������ <see cref="TextWriter"/> ���󥹥��󥹤�ָ�������������֤����Ф����֤� CSV ��ʽ�Υǩ`����������롢
        ///     <see cref="CSVWriter"/> ���饹���¤������󥹥��󥹤���ڻ����ޤ���
        /// </summary>
        /// <param name="writer">�����ȥ饤���`��</param>
        /// <param name="newLine">�������֡�</param>
        /// <param name="delimiter">���Ф����֡�</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer"/> ������ null ���դΈ��ϡ�
        ///     <paramref name="newLine"/> ������ <paramref name="delimiter"/> ������ null ���դ��������ФΈ��ϡ�
        /// </exception>
        public CSVWriter(TextWriter writer, string newLine, string delimiter)
        {
            // ����������å����ޤ���
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (newLine == null || newLine == string.Empty)
            {
                throw new ArgumentNullException("newLine");
            }
            if (delimiter == null || delimiter == string.Empty)
            {
                throw new ArgumentNullException("delimiter");
            }
            // ���ڻ����ޤ���
            this.writer = writer;
            this.newLine = newLine;
            this.delimiter = delimiter;
        }
        /// <summary>
        ///     <see cref="CSVWriter"/> ���󥹥��󥹤��]���ޤ���
        ///     �����ȥ饤���`���]���ޤ���
        /// </summary>
        public void Close()
        {
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
        }
        /// <summary>
        ///     <see cref="CSVWriter"/> ���󥹥��󥹤��]���ޤ���
        ///     �����ȥ饤���`���]���ޤ���
        /// </summary>
        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
        }
        /// <summary>
        ///     <see cref="CSVWriter"/> ���󥹥��󥹤��]���Ƥ��뤫�ɤ�����ȡ�ä��ޤ���
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return (this.writer == null);
            }
        }
        /// <summary>
        ///     �F�ڤγ�����λ�ä������ǥå������Ť�ȡ�ä��ޤ���
        /// </summary>
        /// <remarks>
        ///     ���ڂ�ֱ���λ�ä� 0 �Ǥ���
        /// </remarks>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }
        /// <summary>
        ///     �F�ڤγ�����λ�ä������ǥå������Ť�ȡ�ä��ޤ���
        /// </summary>
        /// <remarks>
        ///     ���ڂ�ֱ���λ�ä� 0 �Ǥ���
        /// </remarks>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
        /// <summary>
        ///     �gһ�΂���������ޤ���
        /// </summary>
        /// <param name="value">�������낎��</param>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> ���󥹥��󥹤��Ȥ��]���Ƥ�����ϡ�</exception>
        public void Write(string value)
        {
            // ���󥹥��󥹤�״�B������å����ޤ���
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            // �������ޤ���
            string v = "";
            if (value == null)
            {
                value = string.Empty;
            }
            if (value.IndexOf(this.newLine) != -1 || value.IndexOf(this.delimiter) != -1)
            {
                v += value.Replace("\"", "\"\"");
                v = "\"" + v + "\"";
            }
            else
            {
                v += value;
            }
            if (this.columnIndex > 0)
            {
                v = this.delimiter + v;
            }
            this.writer.Write(v);
            this.columnIndex++;
        }
        /// <summary>
        ///     ���Ф��ޤ���
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> ���󥹥��󥹤��Ȥ��]���Ƥ�����ϡ�</exception>
        public void WriteNewLine()
        {
            // ���󥹥��󥹤�״�B������å����ޤ���
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            // �������ޤ���
            this.writer.Write(this.newLine);
            this.columnIndex = 0;
            this.rowIndex++;
        }
        /// <summary>
        ///     �}���΂����B�A���Ƴ������ޤ���
        /// </summary>
        /// <param name="values">�������낎�����С�</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> ������ null ���դΈ��ϡ�</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> ���󥹥��󥹤��Ȥ��]���Ƥ�����ϡ�</exception>
        public void Write(string[] values)
        {
            // ����������å����ޤ���
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            // �������ޤ���
            foreach (string value in values)
            {
                this.Write(value);
            }
        }
        /// <summary>
        ///     �}���΂����B�A���Ƴ������ޤ���
        /// </summary>
        /// <param name="values">�������낎�ζ���Ԫ���С�</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> ������ null ���դΈ��ϡ�</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> ���󥹥��󥹤��Ȥ��]���Ƥ�����ϡ�</exception>
        /// <remarks>
        ///     <see cref="Write(string[])"/> �᥽�åɤ��}���΂����B�A���Ƴ������ޤ����� <see cref="Write(string[][])"/> �᥽�åɤ��}���Ф�������ޤ���
        ///     �����ϱؤ��¤����Ф���ʼ�ޤ�ޤ���
        ///     �Ȥ˺Τ餫�΂�����������Ƥ�����ϡ����Ф��Ƥ���������_ʼ���ޤ���
        ///     һ�Ф��Ȥ� (��Ȼ�Ǥ���) ���Ф��ޤ���
        ///     ������Ф���Ф��ޤ���
        ///     �������äơ�ȫ�Ƥγ��������ˤ���ȡ�����λ�ä��¤����Ф����^�ˤʤ�ޤ���
        /// </remarks>
        public void Write(string[][] values)
        {
            // ����������å����ޤ���
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (string[] line in values)
            {
                if (line == null)
                {
                    throw new ArgumentNullException("values");
                }
            }
            // �������ޤ���
            if (this.columnIndex > 0)
            {
                this.WriteNewLine();
            }
            foreach (string[] line in values)
            {
                foreach (string value in line)
                {
                    this.Write(value);
                }
                this.WriteNewLine();
            }
        }
        /// <summary>
        ///     ��`���`���邎���i���z�ߡ�ȫ�Ƴ������ޤ���
        /// </summary>
        /// <param name="dataReader">�����i���z��Ԫ��`���`��</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataReader"/> ������ null ���դΈ��ϡ�</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> ���󥹥��󥹤��Ȥ��]���Ƥ�����ϡ�</exception>
        /// <remarks>
        ///     <see cref="IDataReader"/> ���󥹥��󥹤���ȫ�Ƥ��Ф΂���ȡ�ä���ȫ�Ƥ��Ф�������ޤ���
        ///     �����ϱؤ��¤����Ф���ʼ�ޤ�ޤ���
        ///     �Ȥ˺Τ餫�΂�����������Ƥ�����ϡ����Ф��Ƥ���������_ʼ���ޤ���
        ///     һ�Ф��Ȥ� (��Ȼ�Ǥ���) ���Ф��ޤ���
        ///     ������Ф���Ф��ޤ���
        ///     �������äơ�ȫ�Ƥγ��������ˤ���ȡ�����λ�ä��¤����Ф����^�ˤʤ�ޤ���
        ///     ȫ�Ƥγ��������ˤ��Ƥ� <see cref="IDataReader"/> ���󥹥��󥹤��]�����ޤ���
        /// </remarks>
        public void Write(IDataReader dataReader)
        {
            this.Write(dataReader, null, int.MaxValue);
        }
        /// <summary>
        ///     ��`���`���邎���i���z�ߡ�ָ�������С�����������������ޤ���
        /// </summary>
        /// <param name="dataReader">�����i���z��Ԫ��`���`��</param>
        /// <param name="names">
        ///     �����������������С�
        ///     ȫ�Ƥ��Ф����������Ϥ� null ���դ�ָ�����뤳�Ȥ��Ǥ��ޤ���
        /// </param>
        /// <param name="count">��������������</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataReader"/> ������ null ���դΈ��ϡ�</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> ������ 0 δ���Έ��ϡ�</exception>
        /// <remarks>
        ///     <see cref="IDataReader"/> ���󥹥��󥹤���ָ������혷����Ф΂���ȡ�ä���ָ�������������Ф�������ޤ���
        ///     �����ϱؤ��¤����Ф���ʼ�ޤ�ޤ���
        ///     �Ȥ˺Τ餫�΂�����������Ƥ�����ϡ����Ф��Ƥ���������_ʼ���ޤ���
        ///     һ�Ф��Ȥ� (��Ȼ�Ǥ���) ���Ф��ޤ���
        ///     ������Ф���Ф��ޤ���
        ///     �������äơ�ȫ�Ƥγ��������ˤ���ȡ�����λ�ä��¤����Ф����^�ˤʤ�ޤ���
        ///     ȫ�Ƥγ��������ˤ��Ƥ� <see cref="IDataReader"/> ���󥹥��󥹤��]�����ޤ���
        /// </remarks>
        public void Write(IDataReader dataReader, string[] names, int count)
        {
            // ����������å����ޤ���
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }
            if (count < 0)
            {
                throw new ArgumentException("count ������ 0 δ���Ǥ���");
            }
            // �������ޤ���
            if (this.columnIndex > 0)
            {
                this.WriteNewLine();
            }
            for (int row = 0; row < count; row++)
            {
                if (!dataReader.Read())
                {
                    break;
                }
                object[] values = null;
                if (names != null)
                {
                    values = new object[names.Length];
                    for (int nameIndex = 0; nameIndex < names.Length; nameIndex++)
                    {
                        values[nameIndex] = dataReader[names[nameIndex]];
                    }
                }
                else
                {
                    values = new object[dataReader.FieldCount];
                    dataReader.GetValues(values);
                }
                foreach (object value in values)
                {
                    if (value == DBNull.Value)
                    {
                        this.Write(string.Empty);
                    }
                    else if (value == null)
                    {
                        this.Write(string.Empty);
                    }
                    else
                    {
                        this.Write(value.ToString());
                    }
                }
                this.WriteNewLine();
            }
        }
    }
}