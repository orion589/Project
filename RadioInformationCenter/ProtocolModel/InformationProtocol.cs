using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using Swsu.Lignis.Protocols.BaikalProtocol;
using Swsu.Lignis.Protocols.BaikalProtocol.Messages;
using Swsu.Lignis.Protocols.LignisProtocol.PacketsProcessing;
using Swsu.Lignis.Protocols.ProtocolSdk;

namespace RadioInformationCenter.ProtocolModel
{

    public enum ProtocolType : byte
    {
        Baikal = 0,
        Lignis
    }

    /// <summary>
    /// Тип информации
    /// </summary>
    public enum TypeInformation : byte
    {
        Outcoming = 0,
        Incoming = 1
    }


    public  class InformationProtocol:IBinarySerializable
    {

        #region Constructors
        public InformationProtocol()
        {
        }

        #endregion


        #region Properties

        /// <summary>
        /// Длина пакета
        /// </summary>
        public int PacketLength { get; private set; }


        /// <summary>
        /// Идендификационный номер 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата/Время
        /// </summary>
        public DateTime Time {get; set;}

        /// <summary>
        /// Тип информации (входящая/исходящая)(0-исходящая, 1 - входящая)
        /// </summary>
        public TypeInformation InformationType { get; set; }

        public ProtocolType ProtocolType { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public IBinarySerializable RowData { get; set; }

      #endregion


        /// <summary>
        /// Преобразование информации в байтовый массив
        /// </summary>
        /// <returns></returns>
      public byte[] ToByteArray()
        {
            byte[] ids = Id.ToByteArray();
            byte[] tiks = BitConverter.GetBytes(Time.Ticks);
            byte typs = (byte)InformationType;
            byte[] array = RowData.ToByteArray();

            PacketLength = (int)(ids.Length+tiks.Length+1+array.Length+sizeof(int));
            using (var stream = new MemoryStream()){
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(BitConverter.GetBytes(PacketLength));
                    writer.Write(ids);
                    writer.Write(tiks);
                    writer.Write((byte)ProtocolType);
                    writer.Write((byte)InformationType);
                    writer.Write(array);
                }
                return stream.ToArray();

            }
      }

          /// <summary>
        /// Форминование информации из байтового массива.
        /// </summary>
        /// <param name="_buf"></param>
        /// <param name="_offset"></param>
        public void FromByteArray(byte[] _buf, int _offset)
          {
            PacketLength = BitConverter.ToInt32(_buf, _offset);
            int count = 0;
            using (var stream = new MemoryStream(_buf, _offset, PacketLength))
            {
                using (var reader = new BinaryReader(stream))
                {
                    reader.ReadUInt32();
                    count += sizeof (UInt32);
                    Id = new Guid(reader.ReadBytes(16));
                    count += 16;
                    Time = new DateTime(reader.ReadInt64());
                    count += sizeof (Int64);
                    InformationType = (TypeInformation)reader.ReadByte();
                    count += 1;

                    if (ProtocolType == ProtocolType.Baikal)
                    {
                        var packet1 = new BaikalPacket();
                    }
                    else
                    {
                        var packet2 = PacketsFactory.GeneratePacket(_buf,_offset);
                    }

                    byte[] row = reader.ReadBytes((int) (PacketLength - count));

                    RowData.FromByteArray(_buf, count);
                }
            }
            {
                
            }
      }
    }

}
