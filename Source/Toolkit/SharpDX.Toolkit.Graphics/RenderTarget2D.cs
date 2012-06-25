// Copyright (c) 2010-2012 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

using SharpDX.Direct3D11;

namespace SharpDX.Toolkit.Graphics
{
    public class RenderTarget2D : Texture2DBase
    {
        internal RenderTarget2D(Texture2DDescription description, params DataRectangle[] dataRectangles) : base(description, dataRectangles)
        {
        }

        internal RenderTarget2D(GraphicsDevice device, Texture2DDescription description, params DataRectangle[] dataRectangles)
            : base(device, description, dataRectangles)
        {
        }

        internal RenderTarget2D(Direct3D11.Texture2D texture)
            : base(texture)
        {
        }

        internal RenderTarget2D(GraphicsDevice device, Direct3D11.Texture2D texture)
            : base(device, texture)
        {
        }

        protected override void InitializeViews()
        {
            // Perform default initialization
            base.InitializeViews();

            if ((Description.BindFlags & BindFlags.RenderTarget) != 0)
            {
                RenderTargetViews = new RenderTargetView[GetViewCount()];
                GetRenderTargetView(ViewSlice.Full, 0, 0);
            }
        }

        public override RenderTargetView GetRenderTargetView(ViewSlice viewSlice, int arrayIndex, int mipIndex)
        {
            if ((Description.BindFlags & BindFlags.RenderTarget) == 0)
                return null;

            if (viewSlice == ViewSlice.MipBand)
                throw new NotSupportedException("ViewSlice.MipBand is not supported for render targets");

            int arrayCount;
            int mipCount;
            GetViewSliceBounds(viewSlice, ref arrayIndex, ref mipIndex, out arrayCount, out mipCount);

            var rtvIndex = GetViewIndex(viewSlice, arrayIndex, mipIndex);
            var rtv = RenderTargetViews[rtvIndex];

            // Creates the shader resource view
            if (rtv == null)
            {
                // Create the render target view
                var rtvDescription = new RenderTargetViewDescription() { Format = Description.Format };

                if (Description.ArraySize > 1)
                {
                    rtvDescription.Dimension = Description.SampleDescription.Count > 1 ? RenderTargetViewDimension.Texture2DMultisampledArray : RenderTargetViewDimension.Texture2DArray;
                    if (Description.SampleDescription.Count > 1)
                    {
                        rtvDescription.Texture2DMSArray.ArraySize = arrayCount;
                        rtvDescription.Texture2DMSArray.FirstArraySlice = arrayIndex;
                    }
                    else
                    {
                        rtvDescription.Texture2DArray.ArraySize = arrayCount;
                        rtvDescription.Texture2DArray.FirstArraySlice = arrayIndex;
                        rtvDescription.Texture2DArray.MipSlice = mipIndex;
                    }
                }
                else
                {
                    rtvDescription.Dimension = Description.SampleDescription.Count > 1 ? RenderTargetViewDimension.Texture2DMultisampled : RenderTargetViewDimension.Texture2D;
                    if (Description.SampleDescription.Count <= 1)
                        rtvDescription.Texture2D.MipSlice = mipIndex;
                }

                rtv = new RenderTargetView(GraphicsDevice, Resource, rtvDescription);
                RenderTargetViews[rtvIndex] = ToDispose(rtv);
            }

            return rtv;
        }

        public override Texture2DBase Clone()
        {
            return new RenderTarget2D(GraphicsDevice, Description);
        }

        /// <summary>
        /// Creates a new <see cref="RenderTarget2D"/> from a <see cref="Texture2DDescription"/>.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>
        /// A new instance of <see cref="RenderTarget2D"/> class.
        /// </returns>
        /// <msdn-id>ff476521</msdn-id>	
        /// <unmanaged>HRESULT ID3D11Device::CreateTexture2D([In] const D3D11_TEXTURE2D_DESC* pDesc,[In, Buffer, Optional] const D3D11_SUBRESOURCE_DATA* pInitialData,[Out, Fast] ID3D11Texture2D** ppTexture2D)</unmanaged>	
        /// <unmanaged-short>ID3D11Device::CreateTexture2D</unmanaged-short>	
        public static RenderTarget2D New(Texture2DDescription description)
        {
            return new RenderTarget2D(description);
        }

        /// <summary>
        /// Creates a new <see cref="RenderTarget2D"/> from a <see cref="Direct3D11.Texture2D"/>.
        /// </summary>
        /// <param name="texture">The native texture <see cref="Direct3D11.Texture2D"/>.</param>
        /// <returns>
        /// A new instance of <see cref="RenderTarget2D"/> class.
        /// </returns>
        /// <msdn-id>ff476521</msdn-id>	
        /// <unmanaged>HRESULT ID3D11Device::CreateTexture2D([In] const D3D11_TEXTURE2D_DESC* pDesc,[In, Buffer, Optional] const D3D11_SUBRESOURCE_DATA* pInitialData,[Out, Fast] ID3D11Texture2D** ppTexture2D)</unmanaged>	
        /// <unmanaged-short>ID3D11Device::CreateTexture2D</unmanaged-short>	
        public static RenderTarget2D New(Direct3D11.Texture2D texture)
        {
            return new RenderTarget2D(texture);
        }

        /// <summary>
        /// Creates a new <see cref="RenderTarget2D" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="format">Describes the format to use.</param>
        /// <param name="mipCount">(optional) number of mips.</param>
        /// <param name="arraySize">Size of the texture 2D array, default to 1.</param>
        /// <param name="isUnorderedReadWrite">true if the texture needs to support unordered read write.</param>
        /// <returns>A new instance of <see cref="RenderTarget2D" /> class.</returns>
        /// <msdn-id>ff476521</msdn-id>
        ///   <unmanaged>HRESULT ID3D11Device::CreateTexture2D([In] const D3D11_TEXTURE2D_DESC* pDesc,[In, Buffer, Optional] const D3D11_SUBRESOURCE_DATA* pInitialData,[Out, Fast] ID3D11Texture2D** ppTexture2D)</unmanaged>
        ///   <unmanaged-short>ID3D11Device::CreateTexture2D</unmanaged-short>
        public static RenderTarget2D New(int width, int height, PixelFormat format, bool isUnorderedReadWrite = false, int mipCount = 1, int arraySize = 1)
        {
            return new RenderTarget2D(NewDescription(width, height, format, isUnorderedReadWrite, mipCount, 1));
        }

        /// <summary>
        /// Creates a new <see cref="RenderTarget2D" />.
        /// </summary>
        /// <typeparam name="T">Type of the data contained in the mip map textures.</typeparam>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="format">Describes the format to use.</param>
        /// <param name="isUnorderedReadWrite">true if the texture needs to support unordered read write.</param>
        /// <param name="mipMapTextures">The mip map textures.</param>
        /// <returns>A new instance of <see cref="RenderTarget2D" /> class.</returns>
        /// <msdn-id>ff476521</msdn-id>
        ///   <unmanaged>HRESULT ID3D11Device::CreateTexture2D([In] const D3D11_TEXTURE2D_DESC* pDesc,[In, Buffer, Optional] const D3D11_SUBRESOURCE_DATA* pInitialData,[Out, Fast] ID3D11Texture2D** ppTexture2D)</unmanaged>
        ///   <unmanaged-short>ID3D11Device::CreateTexture2D</unmanaged-short>
        public static RenderTarget2D New<T>(int width, int height, PixelFormat format, T[][] mipMapTextures, bool isUnorderedReadWrite = false) where T : struct
        {
            GCHandle[] handles;
            var dataRectangles = Pin(width, format, mipMapTextures, out handles);
            var texture = new RenderTarget2D(NewDescription(width, height, format, isUnorderedReadWrite, mipMapTextures.Length, 1), dataRectangles);
            UnPin(handles);
            return texture;
        }

        protected static Texture2DDescription NewDescription(int width, int height, PixelFormat format, bool isReadWrite, int mipCount, int arraySize)
        {
            var desc = Texture2DBase.NewDescription(width, height, format, isReadWrite, mipCount, arraySize, ResourceUsage.Default);
            desc.BindFlags |= BindFlags.RenderTarget;
            return desc;
        }
    }
}