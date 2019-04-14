import { Component, OnInit, Input, OnChanges, SimpleChanges, SimpleChange, Output, EventEmitter } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-workout-preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit, OnChanges {
    @Output() goBack = new EventEmitter();

    @Input() recordedReps: RecordedRep[];
    private _recordedReps: RecordedRep[];

    @Input() videoUrl: string;

    private _currentRep: RecordedRep;
    setSeekerLocation: number;
    videoShouldPlay: boolean;
    currentRepNumber: number;

    constructor() { }

    ngOnInit(): void { }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.recordedReps) {
            const recordedReps: SimpleChange = changes.recordedReps;
            this._recordedReps = recordedReps.currentValue;

            if (this._recordedReps.length > 0) {
                this._currentRep = this._recordedReps[0];
                this.setSeekerLocation = this._currentRep.startTime;
                this.videoShouldPlay = true;
                this.currentRepNumber = 1;
            }
        }
    }

    onGoBackClick() {
        this.goBack.emit();
    }

    onVideoReady(event) {
        console.log('ready');
    }

    onVideoError(error) {
        console.log('ready');
    }

    onSetSeekerLocation(event) {
        console.log('set seeker location');
    }

    onVideoProgress(progress) {
        // playedSeconds: 17.313998972697505, played: 0.019843647284933548, loadedSeconds: 0, loaded: 0
        console.log(progress);
        if (progress.played >= this._currentRep.stopTime) {
            // move to next rep
            if (this._recordedReps.length > this.currentRepNumber) {
                this._currentRep = this._recordedReps[this.currentRepNumber];
                this.setSeekerLocation = this._currentRep.startTime;
                this.videoShouldPlay = true;
                this.currentRepNumber += 1;
            } else {
                this.videoShouldPlay = false;
            }
        }
    }

    onVideoPlay(isPlaying) {
    }

    onVideoPause(isPausing) {
    }

}
