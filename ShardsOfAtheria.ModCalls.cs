﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using ShardsOfAtheria.Players;
using System;
using Terraria;

namespace ShardsOfAtheria
{
    partial class ShardsOfAtheria
    {
		// The following code allows other mods to "call" Example Mod data.
		// This allows mod developers to access Example Mod's data without having to set it a reference.
		// Mod calls are not exposed by default, so it will be up to you to publish appropriate calls for your mod, and what values they return.
		public override object Call(params object[] args)
		{
			// Make sure the call doesn't include anything that could potentially cause exceptions.
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
			}

			if (args.Length == 0)
			{
				throw new ArgumentException("Arguments cannot be empty!");
			}

			// This check makes sure that the argument is a string using pattern matching.
			// Since we only need one parameter, we'll take only the first item in the array..
			if (args[0] is string content)
			{
				// ..And treat it as a command type.
				switch (content)
                {
                    default:
						throw new ArgumentException("Unrecognized ModCall. Usable ModCalls for Shards of Atheria are as follows: checkSlayerMode, addNecronomiconEntry and addColoredNecronomiconEntry");
					case "checkSlayerMode":
                        // Checks if the player has Slayer Mode enabled
                        if (args[1] is Player player)
                        {
                            return player.GetModPlayer<SlayerPlayer>().slayerMode;
                        }
                        else
                        {
                            throw new ArgumentException(args[1].GetType().Name + " is not a valid Player type.");
                        }
                    case "addNecronomiconEntry":
						if (args[1] is not string) // Mod Name
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid string.");
						}
						if (args[2] is not string) // Boss Name
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid string.");
						}
						if (args[3] is not string) // Soul Crystal Tooltip
						{
							throw new ArgumentException(args[3].GetType().Name + " is not a valid string.");
						}
						if (args[4] is not int) // Soul Crystal Item ID
						{
							throw new ArgumentException(args[4].GetType().Name + " is not a valid int.");
						}
						Entry.NewEntry((string)args[1], (string)args[2], (string)args[3], (int)args[4]);
						break;
					case "addColoredNecronomiconEntry":
						if (args[1] is not string) // Mod Name
						{
							throw new ArgumentException(args[1].GetType().Name + " is not a valid string.");
						}
						if (args[2] is not string) // Boss Name
						{
							throw new ArgumentException(args[2].GetType().Name + " is not a valid string.");
						}
						if (args[3] is not string) // Soul Crystal Tooltip
						{
							throw new ArgumentException(args[3].GetType().Name + " is not a valid string.");
						}
						if (args[4] is not Color)
						{
							throw new ArgumentException(args[4].GetType().Name + " is not a valid color.");
						}
						if (args[5] is not int) // Soul Crystal Item ID
						{
							throw new ArgumentException(args[5].GetType().Name + " is not a valid int.");
						}
						Entry.NewEntry((string)args[1], (string)args[2], (string)args[3], (Color)args[4], (int)args[5]);
						break;
					case "wipNecronomiconEntry":
						return Entry.WipEntry();
                }
            }

			// If the arguments provided don't match anything we wanted to return a value for, we'll return a 'false' boolean.
			// This value can be anything you would like to provide as a default value.
			return false;
		}
	}
}
