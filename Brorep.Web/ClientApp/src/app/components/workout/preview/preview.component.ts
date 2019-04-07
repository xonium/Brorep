import { Component, OnInit, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-workout-preview',
    templateUrl: './preview.component.html',
    styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit, OnChanges {
    @Input() recordedReps: RecordedRep[];
    private _recordedReps: RecordedRep[];

    @Input() videoUrl: string;

    constructor() { }

    ngOnInit(): void { }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.recordedReps) {
            const recordedReps: SimpleChange = changes.recordedReps;
            this._recordedReps = recordedReps.currentValue;

            console.log('jeessus');
        }
    }

    onVideoReady(event) {
        console.log('ready');
    }

    onVideoError(error) {
        console.log('ready');
    }

    onSetSeekerLocation(event) {
        console.log('ready');
    }

    onVideoProgress(progress) {
        console.log('ready');
    }

    onVideoPlay(isPlaying) {
        console.log('ready');
    }

    onVideoPause(isPausing) {
        console.log('ready');
    }
}
