﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using The_Nth_D.Controller;
using The_Nth_D.Model;

namespace The_Nth_D
{
	public partial class Form1 : Form
	{
		KeysManager keyManager;
		Player player;
		List<Entity> entities = new List<Entity>();
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			DoubleBuffered = true;

			Bitmap playerSprite;
			//playerSprite = new Bitmap(Bitmap.FromFile(@""));
			playerSprite = createBox(100, 100, Color.Black);
			player = new Player(playerSprite, 100, 600);
			entities.Add(player);

			keyManager = new KeysManager(player);

			Random r = new Random();
			Bitmap evilBoxBitmap = createBox(75, 75, Color.Red);

			for (int i = 1; i < 6; i++)
			{
				EvilBox evilBox = new EvilBox(evilBoxBitmap, 100, 100, player, i);
				entities.Add(evilBox);
			}


			Timer gameLoop = new Timer();
			gameLoop.Interval = 10;
			gameLoop.Tick += GameLoop_Tick;
			gameLoop.Start();

		}

		private void GameLoop_Tick(object sender, EventArgs e)
		{
			Invalidate();
			keyManager.handelInput();

			foreach (Entity entity in entities)
			{
				entity.onTick();
			}
		}

		//Method Source https://stackoverflow.com/questions/1720160/how-do-i-fill-a-bitmap-with-a-solid-color
		private Bitmap createBox(int width, int height, Color color)
		{
			Bitmap Bmp = new Bitmap(width, height);
			using (Graphics gfx = Graphics.FromImage(Bmp))
			using (SolidBrush brush = new SolidBrush(color))
			{
				gfx.FillRectangle(brush, 0, 0, width, height);
			}

			return Bmp;
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			setKeyValue(e.KeyCode, false);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			setKeyValue(e.KeyCode, true);
		}

		private void setKeyValue(Keys keyCode, bool value)
		{
			switch (keyCode)
			{
				case Keys.W:
					keyManager.keys[0] = value;
					break;
				case Keys.S:
					keyManager.keys[1] = value;
					break;
				case Keys.A:
					keyManager.keys[2] = value;
					break;
				case Keys.D:
					keyManager.keys[3] = value;
					break;
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;

			graphics.Clear(Color.White);

			foreach (Entity entity in entities)
			{
				entity.Draw(graphics);
			}

		}
	}
}
