using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace hexer
{
    public class DataType : IXmlSerializable
    {
        public bool Seperator { get; internal set; } = false;

        public string Name { get; internal set; }
        public string ShortName { get; internal set; }

        public int NumBytes { get; internal set; }
        public bool VariableNumBytes { get; internal set; }

        private Func<DataFragment, string> decoder;
        private Func<int, string, DataFragment> encoder;

        public DataType(string name, string shortName, int numBytes, bool variableBytes, Func<DataFragment, string> dec, Func<int, string, DataFragment> enc)
        {
            Name = name;
            ShortName = shortName;
            NumBytes = numBytes;
            VariableNumBytes = variableBytes;
            decoder = dec;
            encoder = enc;
        }

        public static DataType FromString(string dts)
        {
            var dt = GetKnownDataTypes().Find(x => x.Name == dts);
            return dt;
        }

        private DataType(string name)
        {
            Name = name;
        }

        // For serialization
        private DataType() { }

        public override string ToString()
        {
            return Name;
        }

        public string DecodeToString(DataFragment target)
        {
            return decoder(target);
        }

        public DataFragment EncodeString(int address, string str)
        {
            return encoder(address, str);
        }

        public static DataType CreateSeparator(string caption)
        {
            DataType ret = new DataType(caption);
            ret.Seperator = true;
            return ret;
        }

        private static List<DataType> dataTypes = null;

        public static List<DataType> GetKnownDataTypesAndSeperators()
        {
            if(dataTypes == null) {
                dataTypes = new List<DataType>();

                dataTypes.Add(DataType.CreateSeparator("Integers"));
                dataTypes.Add(new DataType("int8", "i8", 1, false,
                    df => String.Format("{0:d}", (sbyte)df.Data[0]),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToSByte(str)))
                    ));
                dataTypes.Add(new DataType("int16", "i16", 2, false,
                    df => String.Format("{0:d}", BitConverter.ToInt16(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToInt16(str)))
                    ));
                dataTypes.Add(new DataType("int32", "i32", 4, false,
                    df => String.Format("{0:d}", BitConverter.ToInt32(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToInt32(str)))
                    ));
                dataTypes.Add(new DataType("int64", "i64", 8, false,
                    df => String.Format("{0:d}", BitConverter.ToInt64(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToInt64(str)))
                    ));

                dataTypes.Add(DataType.CreateSeparator("Unsigned"));
                dataTypes.Add(new DataType("uint8", "u8", 1, false,
                    df => String.Format("{0:d}", df.Data[0]),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToByte(str)))
                    ));
                dataTypes.Add(new DataType("uint16", "u16", 2, false,
                    df => String.Format("{0:d}", BitConverter.ToUInt16(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToUInt16(str)))
                    ));
                dataTypes.Add(new DataType("uint32", "u32", 4, false,
                    df => String.Format("{0:d}", BitConverter.ToUInt32(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToUInt32(str)))
                    ));
                dataTypes.Add(new DataType("uint64", "u64", 8, false,
                    df => String.Format("{0:d}", BitConverter.ToUInt64(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToUInt64(str)))
                    ));

                dataTypes.Add(DataType.CreateSeparator("Floats"));
                dataTypes.Add(new DataType("single", "f", 4, false,
                    df => String.Format("{0:f4}", BitConverter.ToSingle(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToSingle(str)))
                    ));
                dataTypes.Add(new DataType("double", "d", 8, false,
                    df => String.Format("{0:f4}", BitConverter.ToDouble(df.Data, 0)),
                    (addr, str) => new DataFragment(addr, BitConverter.GetBytes(Convert.ToDouble(str)))
                    ));

                dataTypes.Add(DataType.CreateSeparator("Text"));
                dataTypes.Add(new DataType("ascii", "ta", 8, true,
                    df => Encoding.ASCII.GetString(df.Data).Truncate(df.Length),
                    (addr, str) => new DataFragment(addr, Encoding.ASCII.GetBytes(str))
                    ));
                dataTypes.Add(new DataType("utf8", "t8", 8, true,
                    df => Encoding.UTF8.GetString(df.Data).Truncate(df.Length),
                    (addr, str) => new DataFragment(addr, Encoding.UTF8.GetBytes(str))
                    ));
                dataTypes.Add(new DataType("utf32", "t32", 8, true,
                    df => Encoding.UTF32.GetString(df.Data).Truncate(df.Length / 4),
                    (addr, str) => new DataFragment(addr, Encoding.UTF32.GetBytes(str))
                    ));
            }
            return dataTypes;
        }

        public static List<DataType> GetKnownDataTypes()
        {
            return GetKnownDataTypesAndSeperators().Where(x => !x.Seperator).ToList();
        }

        public static int StringToAddress(string s)
        {
            if(s.StartsWith("0x")) {
                s = s.Substring(2);
                return (int)Convert.ToUInt32(s, 16);
            }
            else if(s.StartsWith("0b")) {
                s = s.Substring(2);
                return (int)Convert.ToUInt32(s, 2);
            }
            else if(s.StartsWith("0")) {
                s = s.Substring(1);
                return (int)Convert.ToUInt32(s, 8);
            }
            else {
                return Convert.ToInt32(s);
            }
        }
        public static string AddressToString(int addr)
        {
            return "0x" + Convert.ToString(addr, 16).PadLeft(8, '0').ToUpper();
        }

        #region XML Serialization
        public XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            var dt = FromString(reader["type"]);
            Seperator = dt.Seperator;
            Name = dt.Name;
            ShortName = dt.ShortName;
            NumBytes = dt.NumBytes;
            decoder = dt.decoder;
            encoder = dt.encoder;
        }
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("type", Name);
        }
        #endregion
    }

    public class DataFragment
    {
        public const int MAX_LENGTH = 128;
        public byte[] Data { get; internal set; }
        public int Address { get; internal set; }
        public int Length { get; internal set; }

        public DataFragment(int address, byte[] source, int posInSource = 0, int argLength = MAX_LENGTH)
        {
            Address = address;
            int remainder = source.Length - posInSource;
            int length = Math.Min(argLength, remainder);
            Length = length;
            Data = new Byte[MAX_LENGTH];
            Array.Copy(source, posInSource, Data, 0, length);
        }
        public DataFragment()
        {
            Address = 0;
            Length = 0;
            Data = new Byte[MAX_LENGTH];
        }
    }
}
