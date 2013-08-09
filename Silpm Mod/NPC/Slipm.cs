public void AI(){



										if (this.npc.type == 142 && Main.netMode != 1 && !Main.xMas)
										{
											this.npc.StrikeNPC(9999, 0f, 0, false, false);
											if (Main.netMode == 2)
											{
												NetMessage.SendData(28, -1, -1, "", this.npc.whoAmI, 9999f, 0f, 0f, 0);
											}
										}
										int num129 = (int)(this.npc.position.X + (float)(this.npc.width / 2)) / 16;
										int num130 = (int)(this.npc.position.Y + (float)this.npc.height + 1f) / 16;
										if (this.npc.type == 107)
										{
											NPC.savedGoblin = true;
										}
										if (this.npc.type == 108)
										{
											NPC.savedWizard = true;
										}
										if (this.npc.type == 124)
										{
											NPC.savedMech = true;
										}
										if (this.npc.type == 46 && this.npc.target == 255)
										{
											this.npc.TargetClosest(true);
										}
										bool flag15 = false;
										this.npc.directionY = -1;
										if (this.npc.direction == 0)
										{
											this.npc.direction = 1;
										}
										for (int num131 = 0; num131 < 255; num131++)
										{
											if (Main.player[num131].active && Main.player[num131].talkNPC == this.npc.whoAmI)
											{
												flag15 = true;
												if (this.npc.ai[0] != 0f)
												{
													this.npc.netUpdate = true;
												}
												this.npc.ai[0] = 0f;
												this.npc.ai[1] = 300f;
												this.npc.ai[2] = 100f;
												if (Main.player[num131].position.X + (float)(Main.player[num131].width / 2) < this.npc.position.X + (float)(this.npc.width / 2))
												{
													this.npc.direction = -1;
												}
												else
												{
													this.npc.direction = 1;
												}
											}
										}
										if (this.npc.ai[3] > 0f)
										{
											this.npc.life = -1;
											this.npc.HitEffect(0, 10.0);
											this.npc.active = false;
											if (this.npc.type == 37)
											{
												Main.PlaySound(15, (int)this.npc.position.X, (int)this.npc.position.Y, 0);
											}
										}
										if (this.npc.type == 37 && Main.netMode != 1)
										{
											this.npc.homeless = false;
											this.npc.homeTileX = Main.dungeonX;
											this.npc.homeTileY = Main.dungeonY;
											if (NPC.downedBoss3)
											{
												this.npc.ai[3] = 1f;
												this.npc.netUpdate = true;
											}
										}
										int num132 = this.npc.homeTileY;
										if (Main.netMode != 1 && this.npc.homeTileY > 0)
										{
											while (!WorldGen.SolidTile(this.npc.homeTileX, num132) && num132 < Main.maxTilesY - 20)
											{
												num132++;
											}
										}
										if (Main.netMode != 1 && this.npc.townNPC && (!Main.dayTime || Main.tileDungeon[(int)Main.tile[num129, num130].type]) && (num129 != this.npc.homeTileX || num130 != num132) && !this.npc.homeless)
										{
											bool flag16 = true;
											for (int num133 = 0; num133 < 2; num133++)
											{
												Rectangle rectangle3 = new Rectangle((int)(this.npc.position.X + (float)(this.npc.width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(this.npc.position.Y + (float)(this.npc.height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
												if (num133 == 1)
												{
													rectangle3 = new Rectangle(this.npc.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num132 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
												}
												for (int num134 = 0; num134 < 255; num134++)
												{
													if (Main.player[num134].active)
													{
														Rectangle rectangle4 = new Rectangle((int)Main.player[num134].position.X, (int)Main.player[num134].position.Y, Main.player[num134].width, Main.player[num134].height);
														if (rectangle4.Intersects(rectangle3))
														{
															flag16 = false;
															break;
														}
													}
													if (!flag16)
													{
														break;
													}
												}
											}
											if (flag16)
											{
												if (this.npc.type == 37 || !Collision.SolidTiles(this.npc.homeTileX - 1, this.npc.homeTileX + 1, num132 - 3, num132 - 1))
												{
													this.npc.velocity.X = 0f;
													this.npc.velocity.Y = 0f;
													this.npc.position.X = (float)(this.npc.homeTileX * 16 + 8 - this.npc.width / 2);
													this.npc.position.Y = (float)(num132 * 16 - this.npc.height) - 0.1f;
													this.npc.netUpdate = true;
												}
												else
												{
													this.npc.homeless = true;
													WorldGen.QuickFindHome(this.npc.whoAmI);
												}
											}
										}
										if (this.npc.ai[0] == 0f)
										{
											if (this.npc.ai[2] > 0f)
											{
												this.npc.ai[2] -= 1f;
											}
											if (!Main.dayTime && !flag15 && this.npc.type != 46)
											{
												if (Main.netMode != 1)
												{
													if (num129 == this.npc.homeTileX && num130 == num132)
													{
														if (this.npc.velocity.X != 0f)
														{
															this.npc.netUpdate = true;
														}
														if ((double)this.npc.velocity.X > 0.1)
														{
															this.npc.velocity.X = this.npc.velocity.X - 0.1f;
														}
														else
														{
															if ((double)this.npc.velocity.X < -0.1)
															{
																this.npc.velocity.X = this.npc.velocity.X + 0.1f;
															}
															else
															{
																this.npc.velocity.X = 0f;
															}
														}
													}
													else
													{
														if (!flag15)
														{
															if (num129 > this.npc.homeTileX)
															{
																this.npc.direction = -1;
															}
															else
															{
																this.npc.direction = 1;
															}
															this.npc.ai[0] = 1f;
															this.npc.ai[1] = (float)(200 + Main.rand.Next(200));
															this.npc.ai[2] = 0f;
															this.npc.netUpdate = true;
														}
													}
												}
											}
											else
											{
												if ((double)this.npc.velocity.X > 0.1)
												{
													this.npc.velocity.X = this.npc.velocity.X - 0.1f;
												}
												else
												{
													if ((double)this.npc.velocity.X < -0.1)
													{
														this.npc.velocity.X = this.npc.velocity.X + 0.1f;
													}
													else
													{
														this.npc.velocity.X = 0f;
													}
												}
												if (Main.netMode != 1)
												{
													if (this.npc.ai[1] > 0f)
													{
														this.npc.ai[1] -= 1f;
													}
													if (this.npc.ai[1] <= 0f)
													{
														this.npc.ai[0] = 1f;
														this.npc.ai[1] = (float)(200 + Main.rand.Next(200));
														if (this.npc.type == 46)
														{
															this.npc.ai[1] += (float)Main.rand.Next(200, 400);
														}
														this.npc.ai[2] = 0f;
														this.npc.netUpdate = true;
													}
												}
											}
											if (Main.netMode != 1 && (Main.dayTime || (num129 == this.npc.homeTileX && num130 == num132)))
											{
												if (num129 < this.npc.homeTileX - 25 || num129 > this.npc.homeTileX + 25)
												{
													if (this.npc.ai[2] == 0f)
													{
														if (num129 < this.npc.homeTileX - 50 && this.npc.direction == -1)
														{
															this.npc.direction = 1;
															this.npc.netUpdate = true;
															return;
														}
														if (num129 > this.npc.homeTileX + 50 && this.npc.direction == 1)
														{
															this.npc.direction = -1;
															this.npc.netUpdate = true;
															return;
														}
													}
												}
												else
												{
													if (Main.rand.Next(80) == 0 && this.npc.ai[2] == 0f)
													{
														this.npc.ai[2] = 200f;
														this.npc.direction *= -1;
														this.npc.netUpdate = true;
														return;
													}
												}
											}
										}
										else
										{
											if (this.npc.ai[0] == 1f)
											{
												if (Main.netMode != 1 && !Main.dayTime && num129 == this.npc.homeTileX && num130 == this.npc.homeTileY && this.npc.type != 46)
												{
													this.npc.ai[0] = 0f;
													this.npc.ai[1] = (float)(200 + Main.rand.Next(200));
													this.npc.ai[2] = 60f;
													this.npc.netUpdate = true;
													return;
												}
												if (Main.netMode != 1 && !this.npc.homeless && !Main.tileDungeon[(int)Main.tile[num129, num130].type] && (num129 < this.npc.homeTileX - 35 || num129 > this.npc.homeTileX + 35))
												{
													if (this.npc.position.X < (float)(this.npc.homeTileX * 16) && this.npc.direction == -1)
													{
														this.npc.ai[1] -= 5f;
													}
													else
													{
														if (this.npc.position.X > (float)(this.npc.homeTileX * 16) && this.npc.direction == 1)
														{
															this.npc.ai[1] -= 5f;
														}
													}
												}
												this.npc.ai[1] -= 1f;
												if (this.npc.ai[1] <= 0f)
												{
													this.npc.ai[0] = 0f;
													this.npc.ai[1] = (float)(300 + Main.rand.Next(300));
													if (this.npc.type == 46)
													{
														this.npc.ai[1] -= (float)Main.rand.Next(100);
													}
													this.npc.ai[2] = 60f;
													this.npc.netUpdate = true;
												}
												if (this.npc.closeDoor && ((this.npc.position.X + (float)(this.npc.width / 2)) / 16f > (float)(this.npc.doorX + 2) || (this.npc.position.X + (float)(this.npc.width / 2)) / 16f < (float)(this.npc.doorX - 2)))
												{
													bool flag17 = WorldGen.CloseDoor(this.npc.doorX, this.npc.doorY, false);
													if (flag17)
													{
														this.npc.closeDoor = false;
														NetMessage.SendData(19, -1, -1, "", 1, (float)this.npc.doorX, (float)this.npc.doorY, (float)this.npc.direction, 0);
													}
													if ((this.npc.position.X + (float)(this.npc.width / 2)) / 16f > (float)(this.npc.doorX + 4) || (this.npc.position.X + (float)(this.npc.width / 2)) / 16f < (float)(this.npc.doorX - 4) || (this.npc.position.Y + (float)(this.npc.height / 2)) / 16f > (float)(this.npc.doorY + 4) || (this.npc.position.Y + (float)(this.npc.height / 2)) / 16f < (float)(this.npc.doorY - 4))
													{
														this.npc.closeDoor = false;
													}
												}
												if (this.npc.velocity.X < -1f || this.npc.velocity.X > 1f)
												{
													if (this.npc.velocity.Y == 0f)
													{
														this.npc.velocity *= 0.8f;
													}
												}
												else
												{
													if ((double)this.npc.velocity.X < 1.15 && this.npc.direction == 1)
													{
														this.npc.velocity.X = this.npc.velocity.X + 0.07f;
														if (this.npc.velocity.X > 1f)
														{
															this.npc.velocity.X = 1f;
														}
													}
													else
													{
														if (this.npc.velocity.X > -1f && this.npc.direction == -1)
														{
															this.npc.velocity.X = this.npc.velocity.X - 0.07f;
															if (this.npc.velocity.X > 1f)
															{
																this.npc.velocity.X = 1f;
															}
														}
													}
												}
												if (this.npc.velocity.Y == 0f)
												{
													if (this.npc.position.X == this.npc.ai[2])
													{
														this.npc.direction *= -1;
													}
													this.npc.ai[2] = -1f;
													int num135 = (int)((this.npc.position.X + (float)(this.npc.width / 2) + (float)(15 * this.npc.direction)) / 16f);
													int num136 = (int)((this.npc.position.Y + (float)this.npc.height - 16f) / 16f);
													if (Main.tile[num135, num136] == null)
													{
														Main.tile[num135, num136] = new Tile();
													}
													if (Main.tile[num135, num136 - 1] == null)
													{
														Main.tile[num135, num136 - 1] = new Tile();
													}
													if (Main.tile[num135, num136 - 2] == null)
													{
														Main.tile[num135, num136 - 2] = new Tile();
													}
													if (Main.tile[num135, num136 - 3] == null)
													{
														Main.tile[num135, num136 - 3] = new Tile();
													}
													if (Main.tile[num135, num136 + 1] == null)
													{
														Main.tile[num135, num136 + 1] = new Tile();
													}
													if (Main.tile[num135 + this.npc.direction, num136 - 1] == null)
													{
														Main.tile[num135 + this.npc.direction, num136 - 1] = new Tile();
													}
													if (Main.tile[num135 + this.npc.direction, num136 + 1] == null)
													{
														Main.tile[num135 + this.npc.direction, num136 + 1] = new Tile();
													}
													if (this.npc.townNPC && Main.tile[num135, num136 - 2].active && Main.tile[num135, num136 - 2].type == 10 && (Main.rand.Next(10) == 0 || !Main.dayTime))
													{
														if (Main.netMode != 1)
														{
															bool flag18 = WorldGen.OpenDoor(num135, num136 - 2, this.npc.direction);
															if (flag18)
															{
																this.npc.closeDoor = true;
																this.npc.doorX = num135;
																this.npc.doorY = num136 - 2;
																NetMessage.SendData(19, -1, -1, "", 0, (float)num135, (float)(num136 - 2), (float)this.npc.direction, 0);
																this.npc.netUpdate = true;
																this.npc.ai[1] += 80f;
																return;
															}
															if (WorldGen.OpenDoor(num135, num136 - 2, -this.npc.direction))
															{
																this.npc.closeDoor = true;
																this.npc.doorX = num135;
																this.npc.doorY = num136 - 2;
																NetMessage.SendData(19, -1, -1, "", 0, (float)num135, (float)(num136 - 2), (float)(-(float)this.npc.direction), 0);
																this.npc.netUpdate = true;
																this.npc.ai[1] += 80f;
																return;
															}
															this.npc.direction *= -1;
															this.npc.netUpdate = true;
															return;
														}
													}
													else
													{
														if ((this.npc.velocity.X < 0f && this.npc.spriteDirection == -1) || (this.npc.velocity.X > 0f && this.npc.spriteDirection == 1))
														{
															if (Main.tile[num135, num136 - 2].active && Main.tileSolid[(int)Main.tile[num135, num136 - 2].type] && !Main.tileSolidTop[(int)Main.tile[num135, num136 - 2].type])
															{
																if ((this.npc.direction == 1 && !Collision.SolidTiles(num135 - 2, num135 - 1, num136 - 5, num136 - 1)) || (this.npc.direction == -1 && !Collision.SolidTiles(num135 + 1, num135 + 2, num136 - 5, num136 - 1)))
																{
																	if (!Collision.SolidTiles(num135, num135, num136 - 5, num136 - 3))
																	{
																		this.npc.velocity.Y = -6f;
																		this.npc.netUpdate = true;
																	}
																	else
																	{
																		this.npc.direction *= -1;
																		this.npc.netUpdate = true;
																	}
																}
																else
																{
																	this.npc.direction *= -1;
																	this.npc.netUpdate = true;
																}
															}
															else
															{
																if (Main.tile[num135, num136 - 1].active && Main.tileSolid[(int)Main.tile[num135, num136 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num135, num136 - 1].type])
																{
																	if ((this.npc.direction == 1 && !Collision.SolidTiles(num135 - 2, num135 - 1, num136 - 4, num136 - 1)) || (this.npc.direction == -1 && !Collision.SolidTiles(num135 + 1, num135 + 2, num136 - 4, num136 - 1)))
																	{
																		if (!Collision.SolidTiles(num135, num135, num136 - 4, num136 - 2))
																		{
																			this.npc.velocity.Y = -5f;
																			this.npc.netUpdate = true;
																		}
																		else
																		{
																			this.npc.direction *= -1;
																			this.npc.netUpdate = true;
																		}
																	}
																	else
																	{
																		this.npc.direction *= -1;
																		this.npc.netUpdate = true;
																	}
																}
																else
																{
																	if (Main.tile[num135, num136].active && Main.tileSolid[(int)Main.tile[num135, num136].type] && !Main.tileSolidTop[(int)Main.tile[num135, num136].type])
																	{
																		if ((this.npc.direction == 1 && !Collision.SolidTiles(num135 - 2, num135, num136 - 3, num136 - 1)) || (this.npc.direction == -1 && !Collision.SolidTiles(num135, num135 + 2, num136 - 3, num136 - 1)))
																		{
																			this.npc.velocity.Y = -3.6f;
																			this.npc.netUpdate = true;
																		}
																		else
																		{
																			this.npc.direction *= -1;
																			this.npc.netUpdate = true;
																		}
																	}
																}
															}
															try
															{
																if (Main.tile[num135, num136 + 1] == null)
																{
																	Main.tile[num135, num136 + 1] = new Tile();
																}
																if (Main.tile[num135 - this.npc.direction, num136 + 1] == null)
																{
																	Main.tile[num135 - this.npc.direction, num136 + 1] = new Tile();
																}
																if (Main.tile[num135, num136 + 2] == null)
																{
																	Main.tile[num135, num136 + 2] = new Tile();
																}
																if (Main.tile[num135 - this.npc.direction, num136 + 2] == null)
																{
																	Main.tile[num135 - this.npc.direction, num136 + 2] = new Tile();
																}
																if (Main.tile[num135, num136 + 3] == null)
																{
																	Main.tile[num135, num136 + 3] = new Tile();
																}
																if (Main.tile[num135 - this.npc.direction, num136 + 3] == null)
																{
																	Main.tile[num135 - this.npc.direction, num136 + 3] = new Tile();
																}
																if (Main.tile[num135, num136 + 4] == null)
																{
																	Main.tile[num135, num136 + 4] = new Tile();
																}
																if (Main.tile[num135 - this.npc.direction, num136 + 4] == null)
																{
																	Main.tile[num135 - this.npc.direction, num136 + 4] = new Tile();
																}
																else
																{
																	if (num129 >= this.npc.homeTileX - 35 && num129 <= this.npc.homeTileX + 35 && (!Main.tile[num135, num136 + 1].active || !Main.tileSolid[(int)Main.tile[num135, num136 + 1].type]) && (!Main.tile[num135 - this.npc.direction, num136 + 1].active || !Main.tileSolid[(int)Main.tile[num135 - this.npc.direction, num136 + 1].type]) && (!Main.tile[num135, num136 + 2].active || !Main.tileSolid[(int)Main.tile[num135, num136 + 2].type]) && (!Main.tile[num135 - this.npc.direction, num136 + 2].active || !Main.tileSolid[(int)Main.tile[num135 - this.npc.direction, num136 + 2].type]) && (!Main.tile[num135, num136 + 3].active || !Main.tileSolid[(int)Main.tile[num135, num136 + 3].type]) && (!Main.tile[num135 - this.npc.direction, num136 + 3].active || !Main.tileSolid[(int)Main.tile[num135 - this.npc.direction, num136 + 3].type]) && (!Main.tile[num135, num136 + 4].active || !Main.tileSolid[(int)Main.tile[num135, num136 + 4].type]) && (!Main.tile[num135 - this.npc.direction, num136 + 4].active || !Main.tileSolid[(int)Main.tile[num135 - this.npc.direction, num136 + 4].type]) && this.npc.type != 46)
																	{
																		this.npc.direction *= -1;
																		this.npc.velocity.X = this.npc.velocity.X * -1f;
																		this.npc.netUpdate = true;
																	}
																}
															}
															catch
															{
															}
															if (this.npc.velocity.Y < 0f)
															{
																this.npc.ai[2] = this.npc.position.X;
															}
														}
														if (this.npc.velocity.Y < 0f && this.npc.wet)
														{
															this.npc.velocity.Y = this.npc.velocity.Y * 1.2f;
														}
														if (this.npc.velocity.Y < 0f && this.npc.type == 46)
														{
															this.npc.velocity.Y = this.npc.velocity.Y * 1.2f;
															return;
														}
													}
												}
											}
										}
}


public static bool TownSpawn() {

return true;

}

public static string SetName() {

	return "Slipm";
}

public static string Chat() {
int x=Main.rand.Next(4);
	if(x==0){
		return "I'm alchemist!";
}
if(x==1){
		return "Do you need any potions? I'm alchemist!";
}
if(x==2){
		return "This is first Silpm mod... And I'm alchemist!";
}
if(x==3){
		return "Author nick is SILPM, my name is SLIPM... And I'm alchemist!";
}
	return "I have Shiny Red Balloon in my shop, but I'm alchemist!";
}

public static void SetupShop(Chest chest) {
	int index=0;
	chest.item[index].SetDefaults("Shine Potion");
	index++;
	chest.item[index].SetDefaults("Battle Potion");
	index++;
	chest.item[index].SetDefaults("Healing Potion");
	index++;
	chest.item[index].SetDefaults("Archery Potion");
	index++;
	chest.item[index].SetDefaults("Hunter Potion");
	index++;
	chest.item[index].SetDefaults("Magic Power Potion");
	index++;
	chest.item[index].SetDefaults("Spelunker Potion");
	index++;
	chest.item[index].SetDefaults("Gills Potion");
	index++;
	chest.item[index].SetDefaults("Gravitation Potion");
	index++;
	chest.item[index].SetDefaults("Shiny Red Balloon");
	index++;
	chest.item[index].SetDefaults("Obsidian Skin Potion");
	index++;
	//cheated
	/*
	chest.item[index].SetDefaults("Gungnir");
	index++;
	chest.item[index].SetDefaults("Ho-Oh Egg");
	index++;
	chest.item[index].SetDefaults("Gel");
	index++;*/
	
}