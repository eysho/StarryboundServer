﻿/* 
 * Starrybound Server
 * Copyright 2013, Avilance Ltd
 * Created by Zidonuke (zidonuke@gmail.com) and Crashdoom (crashdoom@avilance.com)
 * 
 * This file is a part of Starrybound Server.
 * Starrybound Server is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * Starrybound Server is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Starrybound Server. If not, see http://www.gnu.org/licenses/.
*/

using com.avilance.Starrybound.Packets;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.avilance.Starrybound.Util;
using com.avilance.Starrybound.Extensions;

namespace com.avilance.Starrybound.Commands
{
    class Home : CommandBase
    {
        public Home(ClientThread client)
        {
            this.name = "home";
            this.HelpText = "Woot";

            this.client = client;
            this.player = client.playerData;
        }

        public override bool doProcess(string[] args)
        {
            Packet11ChatSend packet = new Packet11ChatSend(this.client, false, Util.Direction.Client);
            packet.prepare(Util.ChatReceiveContext.White, "", 0, "server", "Teleporting to your home planet.");
            packet.onSend();

            MemoryStream packetWarp = new MemoryStream();
            BinaryWriter packetWrite = new BinaryWriter(packetWarp);

            uint warp = (uint)WarpType.WarpToHomePlanet;
            string sector = "";
            int x = 0;
            int y = 0;
            int z = 0;
            int planet = 0;
            int satellite = 0;
            string player = "";
            packetWrite.WriteBE(warp);
            packetWrite.WriteStarString(sector);
            packetWrite.WriteBE(x);
            packetWrite.WriteBE(y);
            packetWrite.WriteBE(z);
            packetWrite.WriteBE(planet);
            packetWrite.WriteBE(satellite);
            packetWrite.WriteStarString(player);
            client.sendServerPacket(Packet.WarpCommand, packetWarp.ToArray());
            client.sendClientPacket(Packet.WarpCommand, packetWarp.ToArray());

            return true;
        }
    }
}
