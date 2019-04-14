import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { RecordedRep } from 'src/app/models/recordedrep.models';

@Component({
    selector: 'app-workout',
    templateUrl: './workout.component.html',
    styleUrls: ['./workout.component.scss']
})
export class WorkoutComponent implements OnInit {

    step: number;
    videoUrl: string;
    reps: RecordedRep[];

    constructor() {}

    ngOnInit(): void {
        this.step = 0;
        this.reps = [];
    }

    onVideoSelected(videoUrl) {
        this.step = 1;
        this.videoUrl = videoUrl;
    }

    onGoBack() {
        this.step -= 1;
        if (this.step === 0) {
            this.reps = [];
            this.videoUrl = null;
        }
    }

    gotoPreview(event) {
        this.reps = event.reps;
        this.step += 1;
    }
}
