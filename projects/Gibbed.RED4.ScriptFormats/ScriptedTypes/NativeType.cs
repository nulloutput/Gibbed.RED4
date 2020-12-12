﻿/* Copyright (c) 2020 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.IO;
using Gibbed.IO;

namespace Gibbed.RED4.ScriptFormats.ScriptedTypes
{
    public class NativeType : ScriptedType
    {
        public override ScriptedTypeType Type => ScriptedTypeType.Native;

        public byte Unknown24 { get; set; }
        public ScriptedType Unknown18 { get; set; }
        public uint Unknown20 { get; set; }

        internal override void Serialize(Stream output, Endian endian, ICacheTables tables)
        {
            throw new NotImplementedException();
        }

        internal override void Deserialize(Stream input, Endian endian, ICacheTables tables)
        {
            var unknown24 = input.ReadValueU8();
            ScriptedType unknown18;
            if (unknown24 >= 2 && unknown24 <= 6)
            {
                var unknown18Index = input.ReadValueU32(endian);
                unknown18 = tables.GetType(unknown18Index);
            }
            else
            {
                unknown18 = null;
            }
            var unknown20 = unknown24 == 5
                ? input.ReadValueU32(endian)
                : 0;
            this.Unknown24 = unknown24;
            this.Unknown18 = unknown18;
            this.Unknown20 = unknown20;
        }
    }
}
