﻿using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using MithrilShards.Chain.Bitcoin.Protocol.Serialization.Types;

namespace MithrilShards.Chain.Bitcoin.Protocol.Serialization {
   public static class IBufferWriterExtensions {
      public static int WriteBool(this IBufferWriter<byte> writer, bool value) {
         const int size = 1;
         writer.GetSpan(size)[0] = (byte)(value ? 1 : 0);
         writer.Advance(size);
         return size;
      }

      public static int WriteByte(this IBufferWriter<byte> writer, byte value) {
         const int size = 1;
         writer.GetSpan(size)[0] = value;
         writer.Advance(size);
         return 1;
      }

      public static int WriteShort(this IBufferWriter<byte> writer, short value, bool isBigEndian = false) {
         const int size = 2;
         if (isBigEndian) {
            BinaryPrimitives.WriteInt16BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteInt16LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }

      public static int WriteUShort(this IBufferWriter<byte> writer, ushort value, bool isBigEndian = false) {
         const int size = 2;
         if (isBigEndian) {
            BinaryPrimitives.WriteUInt16BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteUInt16LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }


      public static int WriteInt(this IBufferWriter<byte> writer, int value, bool isBigEndian = false) {
         const int size = 4;
         if (isBigEndian) {
            BinaryPrimitives.WriteInt32BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteInt32LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }

      public static int WriteUInt(this IBufferWriter<byte> writer, uint value, bool isBigEndian = false) {
         const int size = 4;
         if (isBigEndian) {
            BinaryPrimitives.WriteUInt32BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteUInt32LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }

      public static int WriteLong(this IBufferWriter<byte> writer, long value, bool isBigEndian = false) {
         const int size = 8;
         if (isBigEndian) {
            BinaryPrimitives.WriteInt64BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteInt64LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }

      public static int WriteULong(this IBufferWriter<byte> writer, ulong value, bool isBigEndian = false) {
         const int size = 8;
         if (isBigEndian) {
            BinaryPrimitives.WriteUInt64BigEndian(writer.GetSpan(size), value);
         }
         else {
            BinaryPrimitives.WriteUInt64LittleEndian(writer.GetSpan(size), value);
         }

         writer.Advance(size);
         return size;
      }

      public static int WriteVarString(this IBufferWriter<byte> writer, string value) {
         int stringLength = value?.Length ?? 0;
         int size = WriteVarInt(writer, (ulong)stringLength);

         Encoding.ASCII.GetBytes(value.AsSpan(), writer.GetSpan(stringLength));
         writer.Advance(stringLength);
         return size + stringLength;
      }

      public static int WriteBytes(this IBufferWriter<byte> writer, byte[] value) {
         if (value is null) {
            throw new ArgumentNullException(nameof(value));
         }

         writer.Write(value);
         return value.Length;
      }

      public static int WriteVarInt(this IBufferWriter<byte> writer, ulong value) {
         if (value < 0xFD) {
            const int size = 1;
            writer.GetSpan(size)[0] = (byte)value;
            writer.Advance(size);
            return size;
         }
         else if (value <= 0xFFFF) {
            const int size = 3;
            Span<byte> span = writer.GetSpan(size);
            span[0] = 0xFD;
            BinaryPrimitives.WriteUInt16LittleEndian(span.Slice(1, size - 1), (ushort)value);
            writer.Advance(size);
            return size;
         }
         else if (value == 0XFFFFFFFF) {
            const int size = 5;
            Span<byte> span = writer.GetSpan(size);
            span[0] = 0xFE;
            BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(1, size - 1), (uint)value);
            writer.Advance(size);
            return size;
         }
         // == 0xFF
         else {
            const int size = 9;
            Span<byte> span = writer.GetSpan(size);
            span[0] = 0xFF;
            BinaryPrimitives.WriteUInt64LittleEndian(span.Slice(1, size - 1), value);
            writer.Advance(size);
            return size;
         }
      }

      /// <summary>
      /// Reads the network address.
      /// </summary>
      /// <param name="reader">The reader.</param>
      /// <param name="skipTimeField">if set to <c>true</c> skips time field serialization/deserialization, used by <see cref="VersionMessage"/>.</param>
      /// <returns></returns>
      public static int WriteNetworkAddress(this IBufferWriter<byte> writer, NetworkAddress value) {
         return value.Serialize(writer);
      }
   }
}
