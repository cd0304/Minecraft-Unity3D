﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Used for both ping and pong.
/// </summary>
public class SpawnMobPacket : Packet
{
	public int EntityID { get; set; }
	public Guid UUID { get; set; }
	public Entity.Type Type { get; set; }
	public double X { get; set; }
	public double Y { get; set; }
	public double Z { get; set; }
	public byte Yaw { get; set; }
	public byte Pitch { get; set; }
	public byte HeadPitch { get; set; }
	public short VelocityX { get; set; }
	public short VelocityY { get; set; }
	public short VelocityZ { get; set; }

	// TODO: add entity metadata

	public SpawnMobPacket()
	{
		PacketID = 0x03;
	}

	public override byte[] Payload
	{
		set
		{
			List<byte> buffer = new List<byte>(value);
			EntityID = VarInt.ReadNext(buffer);
			UUID = PacketHelper.GetGUID(buffer);
			X = PacketHelper.GetDouble(buffer);
			Y = PacketHelper.GetDouble(buffer);
			Z = PacketHelper.GetDouble(buffer);
			Yaw = buffer.Read(1)[0];
			Pitch = buffer.Read(1)[0];
			HeadPitch = buffer.Read(1)[0];
			VelocityX = PacketHelper.GetInt16(buffer);
			VelocityY = PacketHelper.GetInt16(buffer);
			VelocityZ = PacketHelper.GetInt16(buffer);
		}
		get
		{
			throw new NotImplementedException();
		}
	}
}