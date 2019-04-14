import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-videoedit',
    templateUrl: './videoedit.component.html',
    styleUrls: ['./videoedit.component.scss']
})
export class VideoEditComponent implements OnInit {
    @Output() goBack = new EventEmitter();
    @Output() gotoPreview = new EventEmitter();

    @Input() videoUrl: string;
    @Input() recordedReps: RecordedRep[];
    private _recordedReps: RecordedRep[];

    seekerLocation: number;
    setSeekerLocation: number;
    videoReady: boolean;
    isVideoPlaying: boolean;
    videoShouldPlay: boolean;
    isRecording: boolean;
    showPreview: boolean;
    startRecord: number;
    stopRecord: number;

    constructor() { }

    ngOnInit(): void {
        this._recordedReps = this.recordedReps;
    }

    onGoBackClick() {
        this.goBack.emit();
    }

    onVideoPauseClick() {
        this.videoShouldPlay = false;
    }

    onVideoPlayClick() {
        this.videoShouldPlay = true;
    }

    onVideoReady(event) {
        this.videoReady = true;
    }

    onVideoError(error) {
        console.log(error);
    }

    onSetSeekerLocation(event) {
        if (this.isRecording) { return; }

        const target = event.target || event.srcElement || event.currentTarget;
        const value = target.value;
        this.setSeekerLocation = value;
    }

    onVideoProgress(progress) {
        this.seekerLocation = progress.played;
    }

    onVideoPlay(isPlaying) {
        this.isVideoPlaying = isPlaying;
        this.videoShouldPlay = isPlaying;
    }

    onVideoPause(isPausing) {
        this.isVideoPlaying = !isPausing;
        this.videoShouldPlay = !isPausing;
    }

    onVideoRecordClick() {
        this.isRecording = !this.isRecording;

        if (this.isRecording) {
            this.startRecord = this.seekerLocation;
        } else {
            this.stopRecord = this.seekerLocation;

            this._recordedReps.push(
            {
                startTime: this.startRecord,
                stopTime: this.stopRecord,
                length: (this.stopRecord - this.startRecord)
            });

            this._recordedReps.sort((a, b) => {
                let comparison = 0;
                if (a.startTime > b.startTime) {
                    comparison = 1;
                } else {
                    comparison = -1;
                }

                return comparison;
            });
        }
    }

    onVideoPreviewClick() {
        this.showPreview = true;
        this.isVideoPlaying = false;
        this.videoShouldPlay = false;

        this.gotoPreview.emit( {reps: this._recordedReps });
    }
}
