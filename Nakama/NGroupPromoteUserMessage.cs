﻿/**
 * Copyright 2017 The Nakama Authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Google.Protobuf;

namespace Nakama
{
    public class NGroupPromoteUserMessage : INCollatedMessage<bool>
    {
        private Envelope payload;
        public IMessage Payload {
            get {
                return payload;
            }
        }

        private NGroupPromoteUserMessage(byte[] groupId, byte[] userId)
        {
            payload = new Envelope {GroupUsersPromote = new TGroupUsersPromote { GroupUsers =
            {
                new List<TGroupUsersPromote.Types.GroupUserPromote>
                {
                    new TGroupUsersPromote.Types.GroupUserPromote
                    {
                        UserId = ByteString.CopyFrom(userId),
                        GroupId = ByteString.CopyFrom(groupId)
                    }
                }
            }}};     
        }

        public void SetCollationId(string id)
        {
            payload.CollationId = id;
        }

        public override string ToString()
        {
            var p = payload.GroupUsersKick;
            string output = "";
            foreach (var g in p.GroupUsers)
            {
                output += String.Format("(UserId={0},GroupId={1}),", g.UserId, g.GroupId);
            }
            return String.Format("NGroupPromoteUserMessage({0})", output);
        }

        public static NGroupPromoteUserMessage Default(byte[] groupId, byte[] userId)
        {
            return new NGroupPromoteUserMessage(groupId, userId);
        }
    }
}
