<template>
  <div>
    <el-row id="test">
      <el-col :span="12">
        <el-form ref="albumForm" :model="album" label-width="120px" enctype="multipart/form-data">
          <el-form-item label="Album Name" prop="name">
            <el-input v-model="album.name"></el-input>
          </el-form-item>

         <el-form-item>
           <div class="block">
             <span class="demonstration">Default</span>
             <el-date-picker
               v-model="album.releaseDate"
               type="date"
               format="dd.MM.yyyy"
               placeholder="Pick a day">
             </el-date-picker>
           </div>
         </el-form-item>

          <el-form-item>
            <input  ref="images" type="file" @change="previewThumbnail" />
          </el-form-item>
          <div v-if="album.images">
            <img :src="album.images" />
            <button type="button" @click="removeImage">Remove image</button>
          </div>

          <el-form-item>
            <el-select v-model="album.genreId" placeholder="Select">
              <el-option
                v-for="item in genres"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-select v-model="album.artistId" placeholder="Select">
              <el-option
                v-for="item in artist"
                :key="item.id"
                :label="item.name"
                :value="item.id">
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="submitUpload('albumForm')">Create</el-button>
            <el-button>Cancel</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </div>
</template>

<script>
    export default {
      name: 'album-edit',
      created () {
        this.getArtists()
        this.getGenres()
        this.getAlbumDetails()
      },
      methods: {
        submitUpload (formName) {
          const file = this.$refs.images.files
          var formData = new FormData()
          formData.append('name', this.album.name)
          formData.append('releaseDate', this.album.releaseDate)
          formData.append('genreId', this.album.genreId)
          formData.append('artistId', this.album.artistId)
          if (file[0] != null) {
            formData.append('images', file[0], file[0].name)
          }
          this.$http.patch(`http://localhost:5000/api/albums/${this.$route.params.id}`, formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
            .then((response) => { console.log(response.body) }
            , response => { console.log(response.error) })
        },
        getArtists () {
          this.$http.get('http://localhost:5000/api/artists/')
            .then((response) => { this.artist = response.body }
            , (response) => { console.log(response.error) })
        },
        getGenres () {
          this.$http.get('http://localhost:5000/api/genres/')
            .then((response) => { this.genres = response.body }
            , (response) => { console.log(response.error) })
        },
        getAlbumDetails () {
          this.$http.get(`http://localhost:5000/api/albums/${this.$route.params.id}`)
            .then((response) => {
              this.album = response.body
              this.album.images = `http://localhost:5000/${this.album.albumArtURL}`
            })
        },
        previewThumbnail (event) {
          var input = event.target

          if (input.files && input.files[0]) {
            var reader = new FileReader()
            var vm = this

            reader.onload = function (e) {
              vm.album.images = e.target.result
            }

            reader.readAsDataURL(input.files[0])
          }
        },
        removeImage () {
          this.album.images = ''
        }
      },
      computed: {
        dateFormat () {
          let date = new Date(this.album.releaseDate).toISOString()
          this.album.releaseDate = date
        }
      },
      data () {
        return {
          genres: [],
          artist: [],
          album: {
            genreId: null,
            artistId: null,
            releaseDate: '',
            images: ''
          },
          pickerOptions1: {
            shortcuts: [{
              text: 'Today',
              onClick (picker) {
                picker.$emit('pick', new Date())
              }
            }, {
              text: 'Yesterday',
              onClick (picker) {
                const date = new Date()
                date.setTime(date.getTime() - 3600 * 1000 * 24)
                picker.$emit('pick', date)
              }
            }, {
              text: 'A week ago',
              onClick (picker) {
                const date = new Date()
                date.setTime(date.getTime() - 3600 * 1000 * 24 * 7)
                picker.$emit('pick', date)
              }
            }]
          }
        }
      }
    }
</script>

<style scoped>
  #test {
    margin-top: 150px;
  }
</style>
