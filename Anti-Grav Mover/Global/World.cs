public const int NET_CURSOR_DATA         = 100, 
			     NET_ACTIVATE_IAM        = 101, 
				 NET_DEACTIVATE_IAM      = 102, 
				 NET_IAM_ACTIVATION_DATA = 103, 
				 NET_IAM_KILL            = 104,
				 NET_IAM_PULSE           = 105;
public static Vector2[] playerCursor;
public static bool[] playerMouseDown;
public static bool[] iamActive;
public static bool[] pulsing;
public static Vector4[] iamData;
public static int modIndex;
public void Initialize() {
	modIndex = Config.mods.IndexOf("Skiphs Mod");
	playerCursor = new Vector2[Main.maxNetPlayers];
	playerMouseDown = new bool[Main.maxNetPlayers];
	iamData = new Vector4[Main.maxNetPlayers];
	iamActive = new bool[Main.maxNetPlayers];
	pulsing = new bool[Main.maxNetPlayers];
	for (int i = 0; i < playerCursor.Length; i++) {
		playerCursor[i] = default(Vector2);
		iamData[i] = default(Vector4);
		playerMouseDown[i] = false;
		iamActive[i] = false;
		pulsing[i] = false;
	}
}

public void NetReceive(int msg, BinaryReader reader) {
	int id = (int)reader.ReadByte();
	switch (msg) {
		case NET_CURSOR_DATA:
			
			float x = reader.ReadSingle();
			float y = reader.ReadSingle();
			bool mdown = reader.ReadBoolean();
			playerCursor[id].X = x;
			playerCursor[id].Y = y;
			playerMouseDown[id] = mdown;
			if (Main.netMode == 2) {
				NetMessage.SendModData(modIndex, NET_CURSOR_DATA, -1, id, (byte)id, x, y, mdown);
			}
			
			break;
		case NET_ACTIVATE_IAM:
			Console.WriteLine("IAM Activated");
			if (Main.netMode == 2) {
				NetMessage.SendModData(modIndex, NET_ACTIVATE_IAM, -1, id, (byte)id);
			}
			iamActive[id] = true;
			break;
		case NET_DEACTIVATE_IAM:
			for (int i = 0; i < Main.projectile.Length; i++) {
				if (Main.projectile[i].owner == id)
					Main.projectile[i].Kill();
			}
			if (Main.netMode == 2) {
				NetMessage.SendModData(modIndex, NET_DEACTIVATE_IAM, -1, id, (byte)id);
			}
			iamActive[id] = false;
			break;
		case NET_IAM_ACTIVATION_DATA:
			Console.WriteLine("IAM Data received");
			float oDist = reader.ReadSingle();
			int mode = reader.ReadInt32();
			float f1 = reader.ReadSingle();
			float f2 = reader.ReadSingle();
			iamData[id] = new Vector4(oDist, (float)mode, f1, f2);
			
			if (Main.netMode == 2) {
				NetMessage.SendModData(modIndex, NET_IAM_ACTIVATION_DATA, -1, id, (byte)id, oDist, mode, f1, f2);
			}
		break;
		case NET_IAM_PULSE:
			pulsing[id] = true;
			if (Main.netMode == 2) {
				NetMessage.SendModData(modIndex, NET_IAM_PULSE, -1, id, (byte)id);
			}
		break;
	}
}