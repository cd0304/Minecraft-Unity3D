﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct BlockPos
{
	public int X { get; set; }
	public int Y { get; set; }
	public int Z { get; set; }

	/// <summary>
	/// Gets the position of the chunk the block is in
	/// </summary>
	/// <returns></returns>
	public ChunkPos GetChunk()
	{
		return new ChunkPos()
		{
			X = X / 16 - (X < 0 ? 1 : 0),
			Z = Z / 16 - (Z < 0 ? 1 : 0)
		};
	}

	/// <summary>
	/// Gets the block's position relative to the chunk it's in
	/// </summary>
	/// <returns></returns>
	public BlockPos GetPosWithinChunk()
	{
		return new BlockPos()
		{
			X = Utility.Mod(X, 16),
			Y = Y,
			Z = Utility.Mod(Z, 16)
		};
	}

	public BlockPos GetWorldPos(Chunk chunk)
	{
		return new BlockPos()
		{
			X = X + (16 * chunk.Position.X),
			Y = Y,
			Z = Z + (16 * chunk.Position.Z)
		};
	}

	public override string ToString()
	{
		return $"{X}, {Y}, {Z}";
	}

	public override int GetHashCode()
	{
		return $"{X} + {Y} + {Z}".GetHashCode();
	}

	public override bool Equals(object obj)
	{
		BlockPos? pos = obj as BlockPos?;
		if (pos == null)
			return false;
		else
			return (((BlockPos)pos).X == X) && (((BlockPos)pos).Y == Y) && (((BlockPos)pos).Z == Z);
	}
}
