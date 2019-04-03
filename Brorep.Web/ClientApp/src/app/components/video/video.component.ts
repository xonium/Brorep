import { Component, OnInit, Input, SimpleChanges, SimpleChange, EventEmitter, Output, OnChanges } from '@angular/core';
import { RenderReactPlayer } from './reactplayer.standalone';
@Component({
  selector: 'app-video',
  template: `<div class="player-wrapper">
      <div id="player" class="react-player embed-responsive embed-responsive-16by9">
      </div>
    </div>`
})
export class VideoComponent implements OnInit, OnChanges  {
  constructor() { }
  private _reactPlayer: any;

  @Input() videoUrl: string;
  private _videoUrl: string;

  @Input() videoPlay: boolean;
  private _videoPlay: boolean;

  @Input() setSeekerLocation: number;

  @Output() errorEvent = new EventEmitter();
  @Output() progressEvent = new EventEmitter();
  @Output() readyEvent = new EventEmitter();
  @Output() videoPlayEvent = new EventEmitter();
  @Output() videoPauseEvent = new EventEmitter();

  ngOnInit() {
        if (this._videoUrl) {
            const container = document.getElementById('player');
            const url = this.videoUrl;
            RenderReactPlayer(container, { url, playing: true });
        }
    }

    ngOnChanges(changes: SimpleChanges): void {
        const that = this;
        const container = document.getElementById('player');
        const videoSettings = {
            width: '100%',
            height: '100%',
            onPlay: onVideoPlay,
            onProgress: onVideoProgress,
            onError: onVideoError,
            onReady: onVideoReady,
            onPause: onVideoPause,
            ref: (player) => { this._reactPlayer = player; }
        };

        if (changes.videoUrl) {
            const inputvideoUrl: SimpleChange = changes.videoUrl;
            this._videoUrl = inputvideoUrl.currentValue;

            const url = this.videoUrl;

            RenderReactPlayer(container,
                Object.assign(videoSettings,
                    {
                        url,
                        playing: true
                    }));
        }

        if (changes.videoPlay) {
            const inputvideoPlay: SimpleChange = changes.videoPlay;
            this._videoPlay = inputvideoPlay.currentValue;
            const url = this._videoUrl;
            RenderReactPlayer(container,
                Object.assign(videoSettings,
                    {
                        url,
                        playing: this._videoPlay
                    }));
        }

        if (changes.setSeekerLocation) {
            const inputSeekerLocation: SimpleChange = changes.setSeekerLocation;
            if (this._reactPlayer) {
                this._reactPlayer.seekTo(inputSeekerLocation.currentValue);
            }
        }

        function onVideoProgress(progress) {
            that.progressEvent.emit(progress);
        }

        function onVideoPlay() {
            that.videoPlayEvent.emit(true);
        }

        function onVideoError(error) {
            that.errorEvent.emit(error);
        }

        function onVideoReady() {
            that.readyEvent.emit();
        }

        function onVideoPause() {
            that.videoPauseEvent.emit(true);
        }
    }
  }

