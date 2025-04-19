﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace VRCZ.Core.CompiledModels
{
    [EntityFrameworkInternal]
    public partial class AvatarEntityAvatarFavoritesFolderEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "AvatarEntityAvatarFavoritesFolder",
                typeof(Dictionary<string, object>),
                baseEntityType,
                sharedClrType: true,
                indexerPropertyInfo: RuntimeEntityType.FindIndexerProperty(typeof(Dictionary<string, object>)),
                propertyBag: true,
                propertyCount: 2,
                foreignKeyCount: 2,
                unnamedIndexCount: 1,
                keyCount: 1);

            var avatarFavoritesFolderId = runtimeEntityType.AddProperty(
                "AvatarFavoritesFolderId",
                typeof(Guid),
                propertyInfo: runtimeEntityType.FindIndexerPropertyInfo(),
                afterSaveBehavior: PropertySaveBehavior.Throw);

            var avatarsId = runtimeEntityType.AddProperty(
                "AvatarsId",
                typeof(string),
                propertyInfo: runtimeEntityType.FindIndexerPropertyInfo(),
                afterSaveBehavior: PropertySaveBehavior.Throw);

            var key = runtimeEntityType.AddKey(
                new[] { avatarFavoritesFolderId, avatarsId });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { avatarsId });

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("AvatarFavoritesFolderId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey2(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("AvatarsId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "AvatarEntityAvatarFavoritesFolder");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
