import { Component, OnInit, Input, OnChanges, SimpleChanges, SimpleChange, Output, EventEmitter } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-workout-preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit, OnChanges {
    @Output() goBack = new EventEmitter();
    @Output() submit = new EventEmitter();

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
    }

    onVideoError(error) {
    }

    onSetSeekerLocation(event) {
    }

    onVideoProgress(progress) {
        if (progress.played >= this._currentRep.stopTime) {
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

    onSubmitClick() {
        this.submit.emit();
    }

    onReplayClick() {
        if (this._recordedReps.length > 0) {
            this._currentRep = this._recordedReps[0];
            this.setSeekerLocation = this._currentRep.startTime;
            this.videoShouldPlay = true;
            this.currentRepNumber = 1;
        }
    }
}
