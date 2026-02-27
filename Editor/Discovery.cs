/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Unity Technologies.
 *  Copyright (c) BadranRaza.
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;

namespace Microsoft.Unity.VisualStudio.Editor
{
    internal static class Discovery
    {
        public static IEnumerable<IVisualStudioInstallation> GetVisualStudioInstallations()
        {
            foreach (var installation in AntigravityInstallation.GetVisualStudioInstallations())
                yield return installation;
        }

        public static bool TryDiscoverInstallation(string editorPath, out IVisualStudioInstallation installation)
        {
            installation = null;

            if (string.IsNullOrEmpty(editorPath))
                return false;

            try
            {
                if (AntigravityInstallation.TryDiscoverInstallation(editorPath, out installation))
                    return true;
            }
            catch (IOException)
            {
                // do not fail if we are not able to retrieve the exact version number
            }

            return false;
        }

        public static void Initialize()
        {
            AntigravityInstallation.Initialize();
        }
    }
}