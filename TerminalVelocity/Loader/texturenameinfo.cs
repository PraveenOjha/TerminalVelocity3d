
namespace TerVel
{
using System;

public class texturenameinfo 
{
	public String texturename,texturegionname;
    public int x, y, sizex, sizey, orizx, orizy, offsetx, offsety, index;
	
	public texturenameinfo(String texturename, String texturegionname, int x,int y, int sizex, int sizey, int orizx, int orizy,int offsetx, int offsety, int index) 
	
	{
		this.texturename=texturename;
		this.texturegionname=texturegionname;
		this.x=x;
		this.y=y; 
		this.sizex=sizex; 
		this.sizey=sizey; 
		this.orizx=orizx;
		this.orizy=orizy;
		this.offsetx=offsetx;
		this.offsety=offsety; 
		this.index=index;
		
	}

	
	
	
}
}