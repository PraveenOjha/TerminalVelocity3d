
using System;
using System.IO;
using TerVel.Loader;


namespace TerVel
 {
public class loaderparser 
{
static String filepath = "";//"Content\\"; 

public static void loadindex(string file) 
{

	  
	         try
             {
             string line = string.Empty;
             StreamReader sr = new StreamReader(filepath+file);
              parse(sr);
              sr.Close();
             
         }
	  catch (IOException ) 
	  {
          
	  }
	
	
}


private static void parse(StreamReader bf) 
{ 
  string line; 
try 
{
	line = bf.ReadLine();
	
 int index=0;
  while (line != null) 
  {  if(line.Equals(":Packs"))
		  {line = bf.ReadLine();
	     index=1;
		  }
     else if(line.Equals(":Fonts"))
	  {line = bf.ReadLine();
	  index=2;
	  }
      else if(line.Equals(":Music"))
      {line = bf.ReadLine();
      index=3;
      }
      else if(line.Equals(":Sound"))
      {line = bf.ReadLine();
       index=4;
      }
      else if(line.Equals(":patt"))
      {line = bf.ReadLine();
       index=5;
      }
     else 
     { 
	  switch(index)
	  {case 1:packloader(line+".pack");  Assets.loaderp=10;break;
	   case 2:fontloader(line);Assets.loaderp=20;break;
	   case 3:musicloader(line);Assets.loaderp=30;break;
	   case 4:soundloader(line);Assets.loaderp=40;break;
	   case 5:patternloader(line);Assets.loaderp=50;break;
	  }
	  line = bf.ReadLine();
  }
  }
} catch (IOException ) 
{
	
}


}


private static void patternloader(string line)
{
	 
	
}


private static void soundloader(string file) 
{
	 Assets.SoundNames.Add(file);
	 Assets.soundcount++;
	
}


private static void musicloader(string file) 
{
	 Assets.MusicNames.Add(file);
	 Assets.MusicCount++;
	
	// TODO Auto-generated method stub
	
}


private static void fontloader(string file) 
{
 Assets.FontNames.Add(file+".png");
 packloader(file+".pack");
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////.....parse the .pack file and load its data .../////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

private static void packloader( string file)
{
try 
{
StreamReader bf=new StreamReader(filepath+file);
parsepack(bf);
bf.Close();
}
catch (IOException ) 
{
}
	
}


private static void parsepack(StreamReader bf) 
{
	  
	 
	
	string line= null ,texturename = null,texturegionname= null,format= null,filter= null,filter1= null,repeat= null,sub= null,sub1= null,sub2= null;
	Boolean rotate=false;
	int x=0,y=0,sizex=0,sizey=0,orizx=0,orizy = 0,offsetx=0,offsety=0,index=0;
	string [] tokens={".png","format:","filter:","repeat:","rotate:","xy:","size:","orig:","offset:","index:"};
	
    try 
	 {
	 line= bf.ReadLine();
	 while (line != null) 
	 {
	 if(line.Equals(""))
	 line=bf.ReadLine();		 
	
         //st=new StringTokenizer(line,COLON);
	  
	   int i=0;
	   while(i<tokens.Length)
	   { if(line.Contains(tokens[i]))	
	     break; 
	     else
	     i++;
	   }
	   if(line.Equals("")){i++;}
	   
       else if(line.IndexOf(":")!=-1)
	     { sub=line.Substring(line.IndexOf(":")+2);
	       if(line.IndexOf(",")!=-1)
	       {sub1=sub.Substring(0,sub.IndexOf(","));
	       sub2=sub.Substring(sub.IndexOf(",")+2);
	      }
	     
	   }
	   switch(i)
	   {case 0:texturename=line;break;
	    case 1:format=sub;break;
	    case 2:filter=sub1;filter1=sub2;break;
	    case 3:repeat=sub;break;
	    case 4:rotate=Boolean.Parse(sub);break;
	    case 5:x=int.Parse(sub1);y=int.Parse(sub2);break;
	    case 6:sizex=int.Parse(sub1);sizey=int.Parse(sub2);break;
	    case 7:orizx=int.Parse(sub1);orizy=int.Parse(sub2);break;
	    case 8:offsetx=int.Parse(sub1);offsety=int.Parse(sub2);break;
        case 9: index = int.Parse(sub); break;
	    case 10:texturegionname=line;break;
	    default:break;
	  }
	   if(i==3)
	   {
		   Assets.TextureNames.Add(texturename);
		   Assets.texcount++;
	   }
	   if(i==9)
	   {   if(texturegionname.Equals("ghost"))
			 {parseghost(texturename,texturegionname,x,y,sizex,sizey,orizx,orizy,offsetx,offsety,index);
			 }
		   else
		   { texturenameinfo tem=new texturenameinfo(texturename,texturegionname,x,y,sizex,sizey,orizx,orizy,offsetx,offsety,index);
	         if(Assets.TextureRegionNames==null)Assets.TextureRegionNames=new TexturRegionName(); 
	         Assets.TextureRegionNames.add(tem);
		     Assets.texregcount++;
	       }
	   }
	   
	 line=bf.ReadLine();
	 
	 }
	 
	
    }
    
    catch (IOException ) 
    {
	
    }
 
	
	
	
}


private static void parseghost(string texturename, string texturegionname, int x, int y, int sizex, int sizey, int orizx, int orizy, int offsetx, int offsety, int index)
{
	
	int ghostheight=sizey/5;
	int ghostwidth=sizex/6;	
	int nghost=26;
	int i=0;
	while(i<nghost)
	{
	 x=x+ (i%6)*ghostwidth;
	 y=y+(i/5)*ghostheight;
	 texturenameinfo tem=new texturenameinfo(texturename,"ghost_"+i,x,y,ghostwidth,ghostheight,ghostwidth,ghostheight,0,0,-1);
     Assets.TextureRegionNames.add(tem);
     Assets.texregcount++;
     i++;
	}
     
	
}

}
}