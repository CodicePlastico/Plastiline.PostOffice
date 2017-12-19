using System.IO;

namespace Plastiline.PostOffice.Attachments {
    public interface AttachmentProvider {
        Stream ProvideAttachment(string attachmentId);
    }
}