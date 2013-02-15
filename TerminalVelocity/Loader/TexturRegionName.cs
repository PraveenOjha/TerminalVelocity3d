using System;
using System.Collections.Generic;
namespace TerVel

{
	public class TexturRegionName  
	{
		public static List<String> texturename = new List<String>(1000);
		public static List<String> texturegionname= new List<String>(1000);
		public static List<int> x= new List<int>(1000); 
		public static List<int> y= new List<int>(1000); 
		public static List<int> sizex= new List<int>(1000); 
		public static List<int> sizey= new List<int>(1000); 
		public static List<int> orizx= new List<int>(1000); 
		public static List<int> orizy= new List<int>(1000);
		public static List<int> offsetx= new List<int>(1000); 
		public static List<int> offsety= new List<int>(1000); 
		public static List<int> index= new List<int>(1000);
		texturenameinfo temp;
		
		//public TexturRegionName(String texturename, String texturegionname,int x, int y, int sizex, int sizey, int orizx, int orizy,int offsetx, int offsety, int index) 
		//{
		//	temp=new texturenameinfo(texturename, texturegionname, x,  y, sizex,  sizey, orizx, orizy, offsetx,  offsety,  index);	
    	//	}

		
		public bool add(texturenameinfo ob) 
		{
			TexturRegionName.texturename.Add(ob.texturename); 
			TexturRegionName.texturegionname.Add(ob.texturegionname);
			TexturRegionName.x.Add(ob.x); 
			TexturRegionName.y.Add(ob.y); 
			TexturRegionName.sizex.Add(ob.sizex); 
			TexturRegionName.sizey.Add(ob.sizey); 
			TexturRegionName.orizx.Add(ob.orizx); 
			TexturRegionName.orizy.Add(ob.offsety);
			TexturRegionName.offsetx.Add(ob.offsetx); 
			TexturRegionName.offsety.Add(ob.offsety); 
			TexturRegionName.index.Add(ob.index);
			return true;
		}

		public void add(int location,texturenameinfo ob) 
		{TexturRegionName.texturename.Insert(location,ob.texturegionname);
        TexturRegionName.texturegionname.Insert(location, ob.texturegionname);
        TexturRegionName.x.Insert(location, ob.x);
        TexturRegionName.y.Insert(location, ob.y);
        TexturRegionName.sizex.Insert(location, ob.sizex);
        TexturRegionName.sizey.Insert(location, ob.sizey);
        TexturRegionName.orizx.Insert(location, ob.orizx);
        TexturRegionName.orizy.Insert(location, ob.offsety);
        TexturRegionName.offsetx.Insert(location, ob.offsetx);
        TexturRegionName.offsety.Insert(location, ob.offsety);
        TexturRegionName.index.Insert(location, ob.index);
			
		}

		public void clear() 
		{
			// TODO Auto-generated method stub
			
		}

		public Object get(int location)
		{
			// TODO Auto-generated method stub
			return null;
		}


		public int indexOf(Object objecti)
		{
			// TODO Auto-generated method stub
			return 0;
		}



		public int lastIndexOf(Object objecti) 
		{
			return 0;
		}

		public Object remove(int location) 
		{
			// TODO Auto-generated method stub
			return null;
		}

		public int size() 
		{return texturegionname.Count;
		}

	 
		
		 
		
		
	}
}