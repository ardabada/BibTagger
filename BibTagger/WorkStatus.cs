using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibTagger
{
    public enum WorkStatus
    {
        ReadyToStart,
        Working,
        PendingStop
    }

    public enum CurrentOperation
    {
        AwaitingForStart,
        Start,
        ListingAllPhotos,
        PhotoListingDone,
        ReadingParticipantsData,
        ParticipantsDataReadingDone,
        ReadingSavedProgress,
        ProcessingParticipantsBibs,
        ProcessingParticipantsBibsDone,
        ProcessingPhotos,
        TrainingFaces,
        SearchingUnresolvedFaces
    }
}
