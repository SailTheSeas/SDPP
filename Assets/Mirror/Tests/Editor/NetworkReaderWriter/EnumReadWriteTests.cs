using NUnit.Framework;

namespace Mirror.Tests.NetworkReaderWriter
{
    public static class MyCustomEnumReadWrite
    {
        public static void WriteMyCustomEnum(this NetworkWriter networkWriter, EnumReadWriteTests.MyCustomEnum customEnum)
        {
            // if O write N
            if (customEnum == EnumReadWriteTests.MyCustomEnum.O)
            {
                networkWriter.WriteInt((int)EnumReadWriteTests.MyCustomEnum.N);
            }
            else
            {
                networkWriter.WriteInt((int)customEnum);
            }
        }
        public static EnumReadWriteTests.MyCustomEnum ReadMyCustomEnum(this NetworkReader networkReader)
        {
            return (EnumReadWriteTests.MyCustomEnum)networkReader.ReadInt();
        }
    }

    public class EnumReadWriteTests
    {
        public struct ByteMessage : NetworkMessage { public MyByteEnum byteEnum; }
        public enum MyByteEnum : byte
        {
            A, B, C, D
        }

        public struct ShortMessage : NetworkMessage { public MyShortEnum shortEnum; }
        public enum MyShortEnum : short
        {
            E, F, G, H
        }

        public struct CustomMessage : NetworkMessage { public MyCustomEnum customEnum; }
        public enum MyCustomEnum
        {
            M, N, O, P
        }

        [Test]
        public void ByteIsSentForByteEnum()
        {
            ByteMessage msg = new ByteMessage() { byteEnum = MyByteEnum.B };

            NetworkWriter writer = new NetworkWriter();
            writer.Write(msg);

            // should be 1 byte for data
            Assert.That(writer.Position, Is.EqualTo(1));
        }

        [Test]
        public void ShortIsSentForShortEnum()
        {
            ShortMessage msg = new ShortMessage() { shortEnum = MyShortEnum.G };

            NetworkWriter writer = new NetworkWriter();
            writer.Write(msg);

            // should be 2 bytes for data
            Assert.That(writer.Position, Is.EqualTo(2));
        }

        [Test]
        public void CustomWriterIsUsedForEnum()
        {
            CustomMessage serverMsg = new CustomMessage() { customEnum = MyCustomEnum.O };
            CustomMessage clientMsg = SerializeAndDeserializeMessage(serverMsg);

            // custom writer should write N if it sees O
            Assert.That(clientMsg.customEnum, Is.EqualTo(MyCustomEnum.N));
        }

        T SerializeAndDeserializeMessage<T>(T msg)
            where T : struct, NetworkMessage
        {
            NetworkWriter writer = new NetworkWriter();
            writer.Write(msg);
            NetworkReader reader = new NetworkReader(writer.ToArraySegment());
            return reader.Read<T>();
        }
    }
}
