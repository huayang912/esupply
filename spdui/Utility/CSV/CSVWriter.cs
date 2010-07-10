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
    ///     <see cref="TextWriter"/> インスタンスに CSV 形式のデータを出力します。
    /// </summary>
    /// <remarks>
    ///     インスタンスの作成時に指定した改行文字、区切り文字を使用して CSV データを出力します。
    ///     出力する値に改行文字、区切り文字、ダブルクォーテーションが含まれる場合、エスケープして出力します。
    ///     null 参照や <see cref="DBNull.Value"/> 値は空文字列として出力します。
    /// </remarks>
    public sealed class CSVWriter : IDisposable
    {
        /// <summary>
        ///     出力先ライター。
        /// </summary>
        private TextWriter writer;
        /// <summary>
        ///     改行文字。
        /// </summary>
        private string newLine;
        /// <summary>
        ///     区切り文字。
        /// </summary>
        private string delimiter;
        /// <summary>
        ///     出力列位置を表すインデックス番号。
        /// </summary>
        private int columnIndex = 0;
        /// <summary>
        ///     出力行位置を表すインデックス番号。
        /// </summary>
        private int rowIndex = 0;
        /// <summary>
        ///     指定した <see cref="TextWriter"/> インスタンスに CSV 形式のデータを出力する、
        ///     <see cref="CSVWriter"/> クラスの新しいインスタンスを初期化します。
        ///     改行文字は <see cref="Environment.NewLine"/> 、区切り文字は "," になります。
        /// </summary>
        /// <param name="writer">出力先ライター。</param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> 引数が null 参照の場合。</exception>
        public CSVWriter(TextWriter writer)
            : this(writer, Environment.NewLine, ",")
        {
        }
        /// <summary>
        ///     指定した <see cref="TextWriter"/> インスタンスに指定した改行文字と区切り文字で CSV 形式のデータを出力する、
        ///     <see cref="CSVWriter"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="writer">出力先ライター。</param>
        /// <param name="newLine">改行文字。</param>
        /// <param name="delimiter">区切り文字。</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="writer"/> 引数が null 参照の場合。
        ///     <paramref name="newLine"/> 引数、 <paramref name="delimiter"/> 引数が null 参照か空文字列の場合。
        /// </exception>
        public CSVWriter(TextWriter writer, string newLine, string delimiter)
        {
            // 入力をチェックします。
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
            // 初期化します。
            this.writer = writer;
            this.newLine = newLine;
            this.delimiter = delimiter;
        }
        /// <summary>
        ///     <see cref="CSVWriter"/> インスタンスを閉じます。
        ///     出力先ライターも閉じます。
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
        ///     <see cref="CSVWriter"/> インスタンスを閉じます。
        ///     出力先ライターも閉じます。
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
        ///     <see cref="CSVWriter"/> インスタンスが閉じているかどうかを取得します。
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return (this.writer == null);
            }
        }
        /// <summary>
        ///     現在の出力列位置を表すインデックス番号を取得します。
        /// </summary>
        /// <remarks>
        ///     初期値直後の位置は 0 です。
        /// </remarks>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }
        /// <summary>
        ///     現在の出力行位置を表すインデックス番号を取得します。
        /// </summary>
        /// <remarks>
        ///     初期値直後の位置は 0 です。
        /// </remarks>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }
        /// <summary>
        ///     単一の値を出力します。
        /// </summary>
        /// <param name="value">出力する値。</param>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> インスタンスが既に閉じている場合。</exception>
        public void Write(string value)
        {
            // インスタンスの状態をチェックします。
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            // 出力します。
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
        ///     改行します。
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> インスタンスが既に閉じている場合。</exception>
        public void WriteNewLine()
        {
            // インスタンスの状態をチェックします。
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            // 出力します。
            this.writer.Write(this.newLine);
            this.columnIndex = 0;
            this.rowIndex++;
        }
        /// <summary>
        ///     複数の値を連続して出力します。
        /// </summary>
        /// <param name="values">出力する値の配列。</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> 引数が null 参照の場合。</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> インスタンスが既に閉じている場合。</exception>
        public void Write(string[] values)
        {
            // 入力をチェックします。
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            // 出力します。
            foreach (string value in values)
            {
                this.Write(value);
            }
        }
        /// <summary>
        ///     複数の値を連続して出力します。
        /// </summary>
        /// <param name="values">出力する値の二次元配列。</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> 引数が null 参照の場合。</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> インスタンスが既に閉じている場合。</exception>
        /// <remarks>
        ///     <see cref="Write(string[])"/> メソッドは複数の値を連続して出力しますが、 <see cref="Write(string[][])"/> メソッドは複数行を出力します。
        ///     出力は必ず新しい行から始まります。
        ///     既に何らかの値が出力されている場合、改行してから出力を開始します。
        ///     一行ごとに (当然ですが) 改行します。
        ///     最後の行も改行します。
        ///     したがって、全ての出力が完了すると、出力位置は新しい行の先頭になります。
        /// </remarks>
        public void Write(string[][] values)
        {
            // 入力をチェックします。
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
            // 出力します。
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
        ///     リーダーから値を読み込み、全て出力します。
        /// </summary>
        /// <param name="dataReader">値の読み込み元リーダー。</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataReader"/> 引数が null 参照の場合。</exception>
        /// <exception cref="ObjectDisposedException"><see cref="CSVWriter"/> インスタンスが既に閉じている場合。</exception>
        /// <remarks>
        ///     <see cref="IDataReader"/> インスタンスから全ての列の値を取得し、全ての行を出力します。
        ///     出力は必ず新しい行から始まります。
        ///     既に何らかの値が出力されている場合、改行してから出力を開始します。
        ///     一行ごとに (当然ですが) 改行します。
        ///     最後の行も改行します。
        ///     したがって、全ての出力が完了すると、出力位置は新しい行の先頭になります。
        ///     全ての出力が完了しても <see cref="IDataReader"/> インスタンスは閉じられません。
        /// </remarks>
        public void Write(IDataReader dataReader)
        {
            this.Write(dataReader, null, int.MaxValue);
        }
        /// <summary>
        ///     リーダーから値を読み込み、指定した列、及び行数を出力します。
        /// </summary>
        /// <param name="dataReader">値の読み込み元リーダー。</param>
        /// <param name="names">
        ///     出力する列名の配列。
        ///     全ての列を出力する場合は null 参照を指定することができます。
        /// </param>
        /// <param name="count">出力する行数。</param>
        /// <exception cref="ArgumentNullException"><paramref name="dataReader"/> 引数が null 参照の場合。</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> 引数が 0 未満の場合。</exception>
        /// <remarks>
        ///     <see cref="IDataReader"/> インスタンスから指定した順番で列の値を取得し、指定した行数の行を出力します。
        ///     出力は必ず新しい行から始まります。
        ///     既に何らかの値が出力されている場合、改行してから出力を開始します。
        ///     一行ごとに (当然ですが) 改行します。
        ///     最後の行も改行します。
        ///     したがって、全ての出力が完了すると、出力位置は新しい行の先頭になります。
        ///     全ての出力が完了しても <see cref="IDataReader"/> インスタンスは閉じられません。
        /// </remarks>
        public void Write(IDataReader dataReader, string[] names, int count)
        {
            // 入力をチェックします。
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }
            if (count < 0)
            {
                throw new ArgumentException("count 引数が 0 未満です。");
            }
            // 出力します。
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