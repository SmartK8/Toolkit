﻿// Copyright (c) 2010-2012 SharpDX - Alexandre Mutel
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

using SharpDX.Direct3D11;

namespace SharpDX.Toolkit.Graphics
{
    public partial class Buffer
    {
        /// <summary>
        /// Constant buffer helper methods.
        /// </summary>
        public static class Cosntant
        {
            /// <summary>
            /// Creates a new constant buffer with <see cref="ResourceUsage.Dynamic"/> usage.
            /// </summary>
            /// <param name="size">The size in bytes.</param>
            /// <returns>A constant buffer</returns>
            public static Buffer New(int size)
            {
                return Buffer.New(size, BufferFlags.ConstantBuffer, ResourceUsage.Dynamic);
            }

            /// <summary>
            /// Creates a new constant buffer with <see cref="ResourceUsage.Dynamic"/> usage.
            /// </summary>
            /// <typeparam name="T">Type of the constant buffer to get the sizeof from</typeparam>
            /// <returns>A constant buffer</returns>
            public static Buffer New<T>() where T : struct
            {
                return Buffer.New(Utilities.SizeOf<T>(), BufferFlags.ConstantBuffer, ResourceUsage.Dynamic);
            }

            /// <summary>
            /// Creates a new constant buffer with <see cref="ResourceUsage.Dynamic"/> usage.
            /// </summary>
            /// <typeparam name="T">Type of the constant buffer to get the sizeof from</typeparam>
            /// <param name="value">The value to initialize the constant buffer.</param>
            /// <param name="usage">The usage of this resource.</param>
            /// <returns>A constant buffer</returns>
            public static Buffer New<T>(ref T value, ResourceUsage usage = ResourceUsage.Dynamic) where T : struct
            {
                return Buffer.New(ref value, BufferFlags.ConstantBuffer, usage);
            }

            /// <summary>
            /// Creates a new constant buffer with <see cref="ResourceUsage.Dynamic"/> usage.
            /// </summary>
            /// <typeparam name="T">Type of the constant buffer to get the sizeof from</typeparam>
            /// <param name="value">The value to initialize the constant buffer.</param>
            /// <param name="usage">The usage of this resource.</param>
            /// <returns>A constant buffer</returns>
            public static Buffer New<T>(T[] value, ResourceUsage usage = ResourceUsage.Dynamic) where T : struct
            {
                return Buffer.New(value, BufferFlags.ConstantBuffer, usage);
            }

            /// <summary>
            /// Creates a new constant buffer with <see cref="ResourceUsage.Dynamic"/> usage.
            /// </summary>
            /// <param name="value">The value to initialize the constant buffer.</param>
            /// <param name="usage">The usage of this resource.</param>
            /// <returns>A constant buffer</returns>
            public static Buffer New(DataPointer value, ResourceUsage usage = ResourceUsage.Dynamic)
            {
                return Buffer.New(value, 0, BufferFlags.ConstantBuffer, usage);
            }
        }
    }
}