float hash(float2 p)  // replace this by something better
{
    p  = frac( p*0.3183099+.1 );
	p *= 17.0;
    return frac( p.x*p.y*(p.x+p.y) );
}

float hash3(float3 p)  // replace this by something better
{
    p  = frac( p*0.3183099+.1 );
	p *= 17.0;
    return frac( p.x*p.y*p.z*(p.x+p.y+p.z) );
}

void FastNoise_float(float2 x, out float output )
{
    float2 i = floor(x);
    float2 f = frac(x);
    f = f*f*(3.0-2.0*f);
	
    output = lerp(
                    lerp( hash(i+float2(0,0)), hash(i+float2(1,0)),f.x), 
                    lerp( hash(i+float2(0,1)), hash(i+float2(1,1)),f.x),
                    f.y);
}

void FastNoise3d_float(float3 x, out float output )
{
    float3 i = floor(x);
    float3 f = frac(x);
    f = f*f*(3.0-2.0*f);
	
    output = lerp(
                lerp(
                    lerp( hash3(i+float3(0,0,0)), hash3(i+float3(1,0,0)),f.x), 
                    lerp( hash3(i+float3(0,1,0)), hash3(i+float3(1,1,0)),f.x),
                    f.y),

                lerp(
                    lerp( hash3(i+float3(0,0,1)), hash3(i+float3(1,0,1)), f.x),
                    lerp( hash3(i+float3(0,1,1)), hash3(i+float3(1,1,1)),f.x),
                    f.y),
                        
                 f.z);
}

