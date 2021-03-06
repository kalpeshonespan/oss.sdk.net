using System;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using OneSpanSign.API;
using System.IO;
using OneSpanSign.Sdk.Services;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	/// <summary>
	/// The AttachmentRequirementService class provides methods to help create attachments for signers.
	/// </summary>
    public class AttachmentRequirementService
    {
        private AttachmentRequirementApiClient apiClient;
        private PackageService packageService;

        internal AttachmentRequirementService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            packageService = new PackageService(restClient, baseUrl, settings);
            apiClient = new AttachmentRequirementApiClient(restClient, baseUrl, settings);
        }

		/// <summary>
		/// Sender accepts signer's attachment requirement.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public void AcceptAttachment(PackageId packageId, Signer signer, String attachmentName)
        {
            signer.GetAttachmentRequirement(attachmentName).SenderComment = "";
            signer.GetAttachmentRequirement(attachmentName).Status = OneSpanSign.Sdk.RequirementStatus.COMPLETE;
            
            packageService.UpdateSigner(packageId, signer);
        }

		/// <summary>
		/// Sender rejects signer's attachment requirement with a comment.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		/// <param name="senderComment">Sender comment.</param>
        public void RejectAttachment(PackageId packageId, Signer signer, String attachmentName, String senderComment)
        {
            signer.GetAttachmentRequirement(attachmentName).SenderComment = senderComment;
            signer.GetAttachmentRequirement(attachmentName).Status = RequirementStatus.REJECTED;
            
            packageService.UpdateSigner(packageId, signer);
        }

        [Obsolete("This method was replaced by DownloadAttachmentFile")]
        public byte[] DownloadAttachment(PackageId packageId, String attachmentId)
		{
            return DownloadAttachmentFile(packageId, attachmentId).Contents;
		}

        /// <summary>
        /// Sender downloads the attachment file.
        /// </summary>
        /// <returns>The attachment file with file name.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="attachmentId">Attachment identifier.</param>
        public DownloadedFile DownloadAttachmentFile(PackageId packageId, String attachmentId)
        {
            return apiClient.DownloadAttachmentFile(packageId.Id, attachmentId);
        }

        /// <summary>
        /// Sender downloads the attachment file.
        /// </summary>
        /// <returns>The attachment file with file name.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="attachmentId">Attachment identifier.</param>
        /// <param name="attachmentId">Attachment file identifier.</param>
        public DownloadedFile DownloadAttachmentFile(PackageId packageId, String attachmentId, Int32 fileId)
        {
            return apiClient.DownloadAttachmentFile(packageId.Id, attachmentId, fileId);
        }

        [Obsolete("This method was replaced by DownloadAllAttachmentFilesForPackage")]
        public byte[] DownloadAllAttachmentsForPackage(PackageId packageId)
        {
            return DownloadAllAttachmentFilesForPackage(packageId).Contents;
        }

        /// <summary>
        /// Sender downloads all attachment files for the package.
        /// </summary>
        /// <returns>The attachment files with file name.</returns>
        /// <param name="packageId">Package identifier.</param>
        public DownloadedFile DownloadAllAttachmentFilesForPackage(PackageId packageId)
        {
            return apiClient.DownloadAllAttachmentFilesForPackage(packageId.Id);
        }

        [Obsolete("This method was replaced by DownloadAllAttachmentFilesForSignerInPackage")]
        public byte[] DownloadAllAttachmentsForSignerInPackage(DocumentPackage sdkPackage, Signer signer)
        {
            return DownloadAllAttachmentFilesForSignerInPackage(sdkPackage, signer).Contents;
        }

        /// <summary>
        /// Sender downloads all attachment files for the signer in the package.
        /// </summary>
        /// <returns>The attachment files with file name.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="signer">Signer.</param>
        public DownloadedFile DownloadAllAttachmentFilesForSignerInPackage(DocumentPackage sdkPackage, Signer signer)
        {
            return apiClient.DownloadAllAttachmentFilesForSignerInPackage(sdkPackage, signer);
        }

        public void UploadAttachment(PackageId packageId, string attachmentId, string fileName, byte[] fileBytes, string signerSessionId)
        {
            IDictionary<string, byte []> files = new Dictionary<string, byte []> ();
            files.Add (fileName, fileBytes);
            UploadAttachment(packageId, attachmentId, files, signerSessionId);
        }

        public void UploadAttachment (PackageId packageId, string attachmentId, IDictionary<string, byte []> files, string signerSessionId)
        {
            apiClient.UploadAttachment (packageId, attachmentId, files, signerSessionId);
        }

        public void DeleteAttachmentFile(PackageId packageId, string attachmentId, Int32 fileId, string signerSessionId)
        {
            apiClient.DeletaAttachmentFile (packageId, attachmentId, fileId, signerSessionId);
        }
    }
}

