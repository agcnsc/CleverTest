using FFmpeg.AutoGen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CleverTest.Source.FFmpeg
{
    public unsafe class FFmpegManager
    {
        public delegate void ShowBitmap(Bitmap bitmap);

        //private AVCodecContext* pAVCodecCtx_decoder;
        //private AVCodec* pAVCodec_decoder;
        //private AVPacket mAVPacket_decoder = new AVPacket();
        //private AVFrame* pAVFrame_decoder = null;
        //private SwsContext* pImageConvertCtx_decoder = null;
        //private AVFrame* pFrameYUV_decoder = null;

        ///**
        //    H264视频流解码器初始化
        //    @throw exception
        //    @return 初始化成功返回0，否则<0
        //*/
        //unsafe int H264DecoderInit()
        //{
        //    ffmpeg.avcodec_register_all();

        //    var coder = ffmpeg.avcodec_find_decoder(AVCodecID.AV_CODEC_ID_H264);
        //    if (coder == null)
        //    {
        //        throw new Exception("can not find H264 codec");
        //    }

        //    var context = ffmpeg.avcodec_alloc_context3(coder);
        //    if (context == null)
        //    {
        //        throw new Exception("Could not alloc video context!");
        //    }

        //    var paramters = ffmpeg.avcodec_parameters_alloc();
        //    if (ffmpeg.avcodec_parameters_from_context(paramters, context) < 0)
        //    {
        //        ffmpeg.avcodec_parameters_free(&paramters);
        //        ffmpeg.avcodec_free_context(&context);

        //        throw new Exception("Failed to copy avcodec parameters from codec context!");
        //    }

        //    int ret = VideoDecoderInit(paramters);
        //    ffmpeg.avcodec_parameters_free(&paramters);
        //    ffmpeg.avcodec_free_context(&context);

        //    return ret;
        //}


        //unsafe int VideoDecoderInit(AVCodecParameters* parameters)
        //{
        //    if (parameters == null)
        //    {
        //        throw new Exception("Source codec context is NULL");
        //    }

        //    VideoDecoderRelease();
        //    ffmpeg.avcodec_register_all();

        //    pAVCodec_decoder = ffmpeg.avcodec_find_decoder(parameters->codec_id);
        //    if (pAVCodec_decoder == null)
        //    {
        //        throw new Exception("Can not find codec");
        //    }

        //    pAVCodecCtx_decoder = ffmpeg.avcodec_alloc_context3(pAVCodec_decoder);
        //    if (pAVCodecCtx_decoder == null)
        //    {
        //        VideoDecoderRelease();
        //        throw new Exception("Failed to alloc codec context");
        //    }

        //    if (ffmpeg.avcodec_parameters_to_context(pAVCodecCtx_decoder, parameters) < 0)
        //    {
        //        VideoDecoderRelease();
        //        throw new Exception("Failed to avcodec_parameters_to_context");
        //    }

        //    if (ffmpeg.avcodec_open2(pAVCodecCtx_decoder, pAVCodec_decoder, null) < 0)
        //    {
        //        VideoDecoderRelease();
        //        throw new Exception("Failed to avcodec_open2");
        //    }

        //    //IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(mAVPacket_decoder));
        //    //Marshal.StructureToPtr(mAVPacket_decoder, intPtr, true);
        //    //ffmpeg.av_init_packet((AVPacket*)(intPtr.ToPointer()));

        //    fixed (AVPacket* ptr = &mAVPacket_decoder)
        //    {
        //        ffmpeg.av_init_packet(ptr);
        //    }

        //    pAVFrame_decoder = ffmpeg.av_frame_alloc();
        //    pFrameYUV_decoder = ffmpeg.av_frame_alloc();

        //    return 0;
        //}

        //void VideoDecoderRelease()
        //{
        //    if (pAVCodecCtx_decoder != null)
        //    {
        //        fixed (AVCodecContext** ptr = &pAVCodecCtx_decoder)
        //        {
        //            ffmpeg.avcodec_free_context(ptr);
        //        }
        //        pAVCodecCtx_decoder = null;
        //    }

        //    if (pAVFrame_decoder != null)
        //    {
        //        fixed (AVPacket* ptr = &mAVPacket_decoder)
        //        {
        //            ffmpeg.av_packet_unref(ptr);
        //        }

        //        ffmpeg.av_free(pAVFrame_decoder);
        //        pAVFrame_decoder = null;
        //    }

        //    if (pFrameYUV_decoder != null)
        //    {
        //        ffmpeg.av_frame_unref(pFrameYUV_decoder);
        //        ffmpeg.av_free(pFrameYUV_decoder);
        //        pFrameYUV_decoder = null;
        //    }

        //    if (pImageConvertCtx_decoder != null)
        //    {
        //        ffmpeg.sws_freeContext(pImageConvertCtx_decoder);
        //    }

        //    fixed (AVPacket* ptr = &mAVPacket_decoder)
        //    {
        //        ffmpeg.av_packet_unref(ptr);
        //    }
        //}

        //int FFmpeg_H264Decode(unsigned char* inbuf, int inbufSize, int* framePara, unsigned char* outRGBBuf, unsigned char** outYUVBuf)
        //{
        //    if (!pAVCodecCtx_decoder || !pAVFrame_decoder || !inbuf || inbufSize <= 0 || !framePara || (!outRGBBuf && !outYUVBuf))
        //    {
        //        return -1;
        //    }
        //    av_frame_unref(pAVFrame_decoder);
        //    av_frame_unref(pFrameYUV_decoder);

        //    framePara[0] = framePara[1] = 0;
        //    mAVPacket_decoder.data = inbuf;
        //    mAVPacket_decoder.size = inbufSize;

        //    int ret = avcodec_send_packet(pAVCodecCtx_decoder, &mAVPacket_decoder);
        //    if (ret == 0)
        //    {
        //        ret = avcodec_receive_frame(pAVCodecCtx_decoder, pAVFrame_decoder);
        //        if (ret == 0)
        //        {
        //            framePara[0] = pAVFrame_decoder->width;
        //            framePara[1] = pAVFrame_decoder->height;

        //            if (outYUVBuf)
        //            {
        //                *outYUVBuf = (unsigned char*)pAVFrame_decoder->data;
        //                framePara[2] = pAVFrame_decoder->linesize[0];
        //                framePara[3] = pAVFrame_decoder->linesize[1];
        //                framePara[4] = pAVFrame_decoder->linesize[2];
        //            }
        //            else if (outRGBBuf)
        //            {
        //                pFrameYUV_decoder->data[0] = outRGBBuf;
        //                pFrameYUV_decoder->data[1] = NULL;
        //                pFrameYUV_decoder->data[2] = NULL;
        //                pFrameYUV_decoder->data[3] = NULL;
        //                int linesize[4] = { pAVCodecCtx_decoder->width * 3, pAVCodecCtx_decoder->height * 3, 0, 0 };
        //                pImageConvertCtx_decoder = sws_getContext(pAVCodecCtx_decoder->width, pAVCodecCtx_decoder->height, AV_PIX_FMT_YUV420P, pAVCodecCtx_decoder->width, pAVCodecCtx_decoder->height, AV_PIX_FMT_RGB24, SWS_FAST_BILINEAR, NULL, NULL, NULL);
        //                sws_scale(pImageConvertCtx_decoder, (const uint8_t* const *) pAVFrame_decoder->data, pAVFrame_decoder->linesize, 0, pAVCodecCtx_decoder->height, pFrameYUV_decoder->data, linesize);
        //                sws_freeContext(pImageConvertCtx_decoder);

        //                return 1;
        //            }
        //        }
        //        else if (ret == AVERROR(EAGAIN))
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return -1;
        //        }
        //    }

        //    return 0;
        //}


        //=================================================================================================
        AVCodec* pCodec;
        AVCodecContext* pCodecCtx = null;
        AVCodecParserContext* pCodecParserCtx = null;
        AVFrame* pFrame;
        AVPacket packet = new AVPacket();

        public unsafe void Init()
        {
            //FFmpegDLL目录查找和设置
            FFmpegBinariesHelper.RegisterFFmpegBinaries();

            var codec_id = AVCodecID.AV_CODEC_ID_H264;
            ffmpeg.avcodec_register_all();
            pCodec = ffmpeg.avcodec_find_decoder(codec_id);
            if (pCodec == null) throw new ApplicationException(@"Unsupported codec.");

            pCodecCtx = ffmpeg.avcodec_alloc_context3(pCodec);
            if (pCodecCtx == null)
            {
                throw new Exception("Could not alloc video context!");
            }

            pCodecParserCtx = ffmpeg.av_parser_init(Convert.ToInt32(codec_id));
            if (pCodecParserCtx == null)
            {
                throw new Exception("Could not allocate video parser context!");
            }

            if (ffmpeg.avcodec_open2(pCodecCtx, pCodec, null) < 0)
            {
                throw new Exception("Could not open codec!");
            }

            pFrame = ffmpeg.av_frame_alloc();
            fixed (AVPacket* ptr = &packet)
            {
                ffmpeg.av_init_packet(ptr);
            }
        }

        public unsafe void Decode(byte[] data, int size, ShowBitmap show)
        {
            Console.WriteLine("start decode: " + size);
            fixed (byte* ptr = data)
            {
                byte* nPtr = ptr;
                while (size > 0)
                {
                    fixed(byte** pkData = &packet.data)
                    {
                        fixed(int* pkSize = &packet.size)
                        {
                            int len = ffmpeg.av_parser_parse2(
                                   pCodecParserCtx, pCodecCtx,
                                   pkData, pkSize,
                                   nPtr, size, ffmpeg.AV_NOPTS_VALUE, ffmpeg.AV_NOPTS_VALUE, ffmpeg.AV_NOPTS_VALUE);
                            nPtr += len;
                            size -= len;
                            Console.WriteLine("len: "+len+"  size: "+size);

                            if (packet.size == 0)
                            {
                                Console.WriteLine("packet size = 0");
                                continue;
                            }

                            int got_picture;
                            int ret;

                            fixed (AVPacket* pkgPtr = &packet)
                            {
                                ret = ffmpeg.avcodec_decode_video2(pCodecCtx, pFrame, &got_picture, pkgPtr);
                                if (ret < 0)
                                {
                                    throw new Exception("Could not avcodec_decode_video2!");
                                }
                                var width = pCodecCtx->width;
                                var height = pCodecCtx->height;

                                var sourcePixFmt = AVPixelFormat.AV_PIX_FMT_YUV420P;
                                var destinationPixFmt = AVPixelFormat.AV_PIX_FMT_BGR24;

                                // 得到SwsContext对象：用于图像的缩放和转换操作
                                var pConvertContext = ffmpeg.sws_getContext(width, height, sourcePixFmt,
                                    width, height, destinationPixFmt,
                                    ffmpeg.SWS_FAST_BILINEAR, null, null, null);

                                if (pConvertContext == null)
                                    throw new Exception(@"Could not initialize the conversion context.");

                                var dstData = new byte_ptrArray4();
                                var dstLinesize = new int_array4();
                                // 目标媒体格式需要的字节长度
                                var convertedFrameBufferSize = ffmpeg.av_image_get_buffer_size(destinationPixFmt, width, height, 1);
                                // 分配目标媒体格式内存使用
                                var convertedFrameBufferPtr = Marshal.AllocHGlobal(convertedFrameBufferSize);
                                // 设置图像填充参数
                                ffmpeg.av_image_fill_arrays(ref dstData, ref dstLinesize, (byte*)convertedFrameBufferPtr, destinationPixFmt, width, height, 1);


                                ffmpeg.sws_scale(pConvertContext, pFrame->data, pFrame->linesize, 0, height, dstData, dstLinesize);
                                // 封装Bitmap图片
                                var bitmap = new Bitmap(width, height, dstLinesize[0], PixelFormat.Format24bppRgb, convertedFrameBufferPtr);
                                // 回调
                                var cpyBmp = DeepClone(bitmap);
                                show(cpyBmp);

                                Marshal.FreeHGlobal(convertedFrameBufferPtr);
                            }
                        }
                    }
                }
            }

        }

        public unsafe void Release()
        {
            ffmpeg.av_parser_close(pCodecParserCtx);

            fixed (AVFrame** ptr = &pFrame)
            {
                ffmpeg.av_frame_unref(pFrame);
                ffmpeg.av_frame_free(ptr);
            }

            fixed (AVPacket* pkgPtr = &packet)
            {
                ffmpeg.av_packet_unref(pkgPtr);
            }

            ffmpeg.avcodec_close(pCodecCtx);
            ffmpeg.av_free(pCodecCtx);

        }

        public Bitmap DeepClone(Bitmap bitmap)
        {
            Bitmap dstBitmap = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mStream, bitmap);
                mStream.Seek(0, SeekOrigin.Begin);//指定当前流的位置为流的开头。
                dstBitmap = (Bitmap)bf.Deserialize(mStream);
                mStream.Close();
            }
            return dstBitmap;
        }


        //=================================================================================================
        ///// <summary>
        ///// 执行控制变量
        ///// </summary>
        //bool CanRun;

        //public unsafe void Start(string url, ShowBitmap show)
        //{
        //    CanRun = true;

        //    #region ffmpeg 初始化
        //    //FFmpegDLL目录查找和设置
        //    FFmpegBinariesHelper.RegisterFFmpegBinaries();

        //    // 初始化注册ffmpeg相关的编码器
        //    ffmpeg.av_register_all();
        //    ffmpeg.avcodec_register_all();
        //    ffmpeg.avformat_network_init();

        //    Console.WriteLine($"FFmpeg version info: {ffmpeg.av_version_info()}");
        //    #endregion

        //    #region ffmpeg 日志
        //    // 设置记录ffmpeg日志级别
        //    ffmpeg.av_log_set_level(ffmpeg.AV_LOG_VERBOSE);
        //    av_log_set_callback_callback logCallback = (p0, level, format, vl) =>
        //    {
        //        if (level > ffmpeg.av_log_get_level()) return;

        //        var lineSize = 1024;
        //        var lineBuffer = stackalloc byte[lineSize];
        //        var printPrefix = 1;
        //        ffmpeg.av_log_format_line(p0, level, format, vl, lineBuffer, lineSize, &printPrefix);
        //        var line = Marshal.PtrToStringAnsi((IntPtr)lineBuffer);
        //        Console.Write(line);
        //    };
        //    ffmpeg.av_log_set_callback(logCallback);

        //    #endregion

        //    #region ffmpeg 转码


        //    // 分配音视频格式上下文
        //    var pFormatContext = ffmpeg.avformat_alloc_context();

        //    int error;

        //    //打开流
        //    error = ffmpeg.avformat_open_input(&pFormatContext, url, null, null);
        //    if (error != 0) throw new ApplicationException(GetErrorMessage(error));

        //    // 读取媒体流信息
        //    error = ffmpeg.avformat_find_stream_info(pFormatContext, null);
        //    if (error != 0) throw new ApplicationException(GetErrorMessage(error));

        //    // 这里只是为了打印些视频参数
        //    AVDictionaryEntry* tag = null;
        //    while ((tag = ffmpeg.av_dict_get(pFormatContext->metadata, "", tag, ffmpeg.AV_DICT_IGNORE_SUFFIX)) != null)
        //    {
        //        var key = Marshal.PtrToStringAnsi((IntPtr)tag->key);
        //        var value = Marshal.PtrToStringAnsi((IntPtr)tag->value);
        //        Console.WriteLine($"{key} = {value}");
        //    }

        //    // 从格式化上下文获取流索引
        //    AVStream* pStream = null, aStream;
        //    for (var i = 0; i < pFormatContext->nb_streams; i++)
        //    {
        //        if (pFormatContext->streams[i]->codec->codec_type == AVMediaType.AVMEDIA_TYPE_VIDEO)
        //        {
        //            pStream = pFormatContext->streams[i];

        //        }
        //        else if (pFormatContext->streams[i]->codec->codec_type == AVMediaType.AVMEDIA_TYPE_AUDIO)
        //        {
        //            aStream = pFormatContext->streams[i];

        //        }
        //    }
        //    if (pStream == null) throw new ApplicationException(@"Could not found video stream.");

        //    // 获取流的编码器上下文
        //    var codecContext = *pStream->codec;

        //    Console.WriteLine($"codec name: {ffmpeg.avcodec_get_name(codecContext.codec_id)}");
        //    // 获取图像的宽、高及像素格式
        //    var width = codecContext.width;
        //    var height = codecContext.height;
        //    var sourcePixFmt = codecContext.pix_fmt;

        //    // 得到编码器ID
        //    var codecId = codecContext.codec_id;
        //    // 目标像素格式
        //    var destinationPixFmt = AVPixelFormat.AV_PIX_FMT_BGR24;


        //    // 某些264格式codecContext.pix_fmt获取到的格式是AV_PIX_FMT_NONE 统一都认为是YUV420P
        //    if (sourcePixFmt == AVPixelFormat.AV_PIX_FMT_NONE && codecId == AVCodecID.AV_CODEC_ID_H264)
        //    {
        //        sourcePixFmt = AVPixelFormat.AV_PIX_FMT_YUV420P;
        //    }

        //    // 得到SwsContext对象：用于图像的缩放和转换操作
        //    var pConvertContext = ffmpeg.sws_getContext(width, height, sourcePixFmt,
        //        width, height, destinationPixFmt,
        //        ffmpeg.SWS_FAST_BILINEAR, null, null, null);
        //    if (pConvertContext == null) throw new ApplicationException(@"Could not initialize the conversion context.");

        //    //分配一个默认的帧对象:AVFrame
        //    var pConvertedFrame = ffmpeg.av_frame_alloc();
        //    // 目标媒体格式需要的字节长度
        //    var convertedFrameBufferSize = ffmpeg.av_image_get_buffer_size(destinationPixFmt, width, height, 1);
        //    // 分配目标媒体格式内存使用
        //    var convertedFrameBufferPtr = Marshal.AllocHGlobal(convertedFrameBufferSize);
        //    var dstData = new byte_ptrArray4();
        //    var dstLinesize = new int_array4();
        //    // 设置图像填充参数
        //    ffmpeg.av_image_fill_arrays(ref dstData, ref dstLinesize, (byte*)convertedFrameBufferPtr, destinationPixFmt, width, height, 1);

        //    #endregion

        //    #region ffmpeg 解码
        //    // 根据编码器ID获取对应的解码器
        //    var pCodec = ffmpeg.avcodec_find_decoder(codecId);
        //    if (pCodec == null) throw new ApplicationException(@"Unsupported codec.");

        //    var pCodecContext = &codecContext;

        //    if ((pCodec->capabilities & ffmpeg.AV_CODEC_CAP_TRUNCATED) == ffmpeg.AV_CODEC_CAP_TRUNCATED)
        //        pCodecContext->flags |= ffmpeg.AV_CODEC_FLAG_TRUNCATED;

        //    // 通过解码器打开解码器上下文:AVCodecContext pCodecContext
        //    error = ffmpeg.avcodec_open2(pCodecContext, pCodec, null);
        //    if (error < 0) throw new ApplicationException(GetErrorMessage(error));

        //    // 分配解码帧对象：AVFrame pDecodedFrame
        //    var pDecodedFrame = ffmpeg.av_frame_alloc();

        //    // 初始化媒体数据包
        //    var packet = new AVPacket();
        //    var pPacket = &packet;
        //    ffmpeg.av_init_packet(pPacket);

        //    var frameNumber = 0;
        //    while (CanRun)
        //    {
        //        try
        //        {
        //            do
        //            {
        //                // 读取一帧未解码数据
        //                error = ffmpeg.av_read_frame(pFormatContext, pPacket);
        //                // Console.WriteLine(pPacket->dts);
        //                if (error == ffmpeg.AVERROR_EOF) break;
        //                if (error < 0) throw new ApplicationException(GetErrorMessage(error));

        //                if (pPacket->stream_index != pStream->index) continue;

        //                // 解码
        //                error = ffmpeg.avcodec_send_packet(pCodecContext, pPacket);
        //                if (error < 0) throw new ApplicationException(GetErrorMessage(error));
        //                // 解码输出解码数据
        //                error = ffmpeg.avcodec_receive_frame(pCodecContext, pDecodedFrame);
        //            } while (error == ffmpeg.AVERROR(ffmpeg.EAGAIN) && CanRun);
        //            if (error == ffmpeg.AVERROR_EOF) break;
        //            if (error < 0) throw new ApplicationException(GetErrorMessage(error));

        //            if (pPacket->stream_index != pStream->index) continue;

        //            //Console.WriteLine($@"frame: {frameNumber}");
        //            // YUV->RGB
        //            ffmpeg.sws_scale(pConvertContext, pDecodedFrame->data, pDecodedFrame->linesize, 0, height, dstData, dstLinesize);
        //        }
        //        finally
        //        {
        //            ffmpeg.av_packet_unref(pPacket);//释放数据包对象引用
        //            ffmpeg.av_frame_unref(pDecodedFrame);//释放解码帧对象引用
        //        }

        //        // 封装Bitmap图片
        //        var bitmap = new Bitmap(width, height, dstLinesize[0], PixelFormat.Format24bppRgb, convertedFrameBufferPtr);
        //        // 回调
        //        show(bitmap);
        //        //bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + "\\264\\frame.buffer."+ frameNumber + ".jpg", ImageFormat.Jpeg);

        //        frameNumber++;
        //    }
        //    //播放完置空播放图片 
        //    show(null);

        //    #endregion

        //    #region 释放资源
        //    Marshal.FreeHGlobal(convertedFrameBufferPtr);
        //    ffmpeg.av_free(pConvertedFrame);
        //    ffmpeg.sws_freeContext(pConvertContext);

        //    ffmpeg.av_free(pDecodedFrame);
        //    ffmpeg.avcodec_close(pCodecContext);
        //    ffmpeg.avformat_close_input(&pFormatContext);


        //    #endregion
        //}

        //private static unsafe string GetErrorMessage(int error)
        //{
        //    var bufferSize = 1024;
        //    var buffer = stackalloc byte[bufferSize];
        //    ffmpeg.av_strerror(error, buffer, (ulong)bufferSize);
        //    var message = Marshal.PtrToStringAnsi((IntPtr)buffer);
        //    return message;
        //}

        //public void Stop()
        //{
        //    CanRun = false;
        //}
    }
}
