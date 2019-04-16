import { Component, OnInit } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-submit',
    templateUrl: './submit.component.html',
    styleUrls: ['./submit.component.scss']
})
export class SubmitComponent implements OnInit {
    step: number;
    videoUrl: string;
    workoutGuid: string;
    reps: RecordedRep[];

    constructor() {}

    ngOnInit(): void {
        this.step = 0;
        this.reps = [];
    }

    onGoBack() {
        this.step -= 1;
        if (this.step === 0) {
            this.reps = [];
            this.videoUrl = null;
        }
    }

    onWorkoutSelected(workoutGuid) {
        this.step += 1;
        this.workoutGuid = workoutGuid;
    }

    onVideoSelected(videoUrl) {
        this.step += 1;
        this.videoUrl = videoUrl;
    }

    gotoPreview(event) {
        this.reps = event.reps;
        this.step += 1;
    }
}
